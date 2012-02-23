using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Chaos.Util
{
	/// <summary>
	/// Delegate that is used to fill the int array with random data
	/// </summary>
	/// <param name="randomData">the array to fill</param>
	public delegate void RandomIntDataProvider(int[] randomData);
	public delegate void RandomByteDataProvider(byte[] randomData);

	/// <summary>
	/// The default implementation for IRandomGen
	/// </summary>
	public class RandomGen : IRandomGen
	{
		private readonly RandomIntDataProvider provider;

		protected readonly int[] buffer;
		protected int index;
		private uint bitStore;
		private uint byteStore;
		private uint shortStore;
		private double gaussStore;


		[ContractInvariantMethod]
		void ObjectInvariant()
		{
			Contract.Invariant(provider != null);
			Contract.Invariant(buffer != null);
			Contract.Invariant(index >= 0);
			Contract.Invariant(index <= buffer.Length);
		}

		private void Refill()
		{
			provider(buffer);
		}

		protected void Require(int ints)
		{
			Contract.Requires(ints >= 0);
			Contract.Requires(ints <= buffer.Length);
			index -= ints;
			if (index < 0)
			{
				Refill();
				index = buffer.Length - ints;
			}
		}

		public double Uniform()
		{
			ulong value = UInt64();
			//chosen so that result < 1 even when value&mask==mask
			const ulong count = 1ul << 53;
			const ulong mask = count - 1;
			const double multiplier = (1 / (double)count);//Exactly representable
			double result = (value & mask) * multiplier;
			return result;
		}

		public double UniformStartEnd(double start, double exclusiveEnd)
		{
			double result;
			do
			{
				// The seemingly redundant cast is necessary to coerce the value to double
				// else the result might have a higher precision
				result = (double)((exclusiveEnd - start) * Uniform() + start);
			} while (result >= exclusiveEnd);
			// The loop is necessary, because rounding errors might push the result so it's equal to exclusiveEnd, which would violate the contract
			// But that event is *very* rare
			return result;
		}

		public double UniformStartLength(double start, double length)
		{
			return length * Uniform() + start;
		}

		public float UniformSingle()
		{
			uint value = UInt32();
			//chosen so that result < 1 even when value&mask==mask
			const ulong count = 1ul << 24;
			const ulong mask = count - 1;
			const float multiplier = (1 / (float)count);//Exactly representable
			float result = (value & mask) * multiplier;
			return result;
		}

		public float UniformSingleStartEnd(float start, float exclusiveEnd)
		{
			float result;
			do
			{
				// The seemingly redundant cast is necessary to coerce the value to float
				// else the result might have a higher precision
				result = (float)((exclusiveEnd - start) * Uniform() + start);
			} while (result >= exclusiveEnd);
			// The loop is necessary, because rounding errors might push the result so it's equal to exclusiveEnd, which would violate the contract
			// But that event is *very* rare
			return result;
		}

		public float UniformSingleStartLength(float start, float length)
		{
			return length * UniformSingle() + start;
		}

		public double Gaussian()
		{
			double oldGauss = this.gaussStore;
			if (!double.IsNaN(oldGauss))
			{
				this.gaussStore = double.NaN;
				return oldGauss;
			}
			else
			{
				double u = Uniform();
				double v = Uniform();

				double radius = Math.Sqrt(-2 * Math.Log(u));
				double angle = (2 * Math.PI) * v;

				double gauss1 = radius * Math.Cos(angle);
				double gauss2 = radius * Math.Sin(angle);
				this.gaussStore = gauss2;
				return gauss1;
			}
		}

		public double Gaussian(double standardDeviation)
		{
			return Gaussian() * standardDeviation;
		}

		public double Gaussian(double mean, double standardDeviation)
		{
			return Gaussian() * standardDeviation + mean;
		}

		public RandomGen(RandomByteDataProvider provider, int bufferSizeInBytes)
		{
			Contract.Requires(bufferSizeInBytes >= 8);
			Contract.Requires(bufferSizeInBytes % 4 == 0);
			Contract.Requires(provider != null);
			byte[] byteBuffer = new byte[bufferSizeInBytes];
			this.buffer = new int[bufferSizeInBytes / 4];
			this.provider = (int[] randomData) =>
				{
					provider(byteBuffer);
					Buffer.BlockCopy(byteBuffer, 0, buffer, 0, bufferSizeInBytes);
				};
		}

		public RandomGen(RandomIntDataProvider provider, int bufferSizeInBytes)
		{
			Contract.Requires(bufferSizeInBytes >= 8);
			Contract.Requires(bufferSizeInBytes % 4 == 0);
			Contract.Requires(provider != null);
			this.provider = provider;
			this.buffer = new int[bufferSizeInBytes / 4];
		}

		public double Exponential()
		{
			double u = Uniform();
			return Math.Log(u);
		}


		public double ExponentialMean(double mean)
		{
			return Exponential() * mean;
		}

		public double ExponentialRate(double rate)
		{
			return Exponential() / rate;
		}

		public int Poisson(double mean)
		{
			throw new NotImplementedException();
		}

		public bool Bool()
		{
			var localBitStore = bitStore;
			if (localBitStore > 1)
			{
				bitStore = localBitStore >> 1;
				return (bitStore & 1) != 0;
			}
			else
			{
				localBitStore = UInt32();
				bitStore = localBitStore | 0x80000000;
				return (int)localBitStore < 0;
			}
		}

		public bool Bool(double probability)
		{
			return Uniform() < probability;
		}

		public sbyte SByte()
		{
			return (sbyte)Byte();
		}

		public byte Byte()
		{
			var localByteStore = byteStore;
			if (localByteStore >= 0x100)
			{
				byteStore = localByteStore >> 8;
				return (byte)localByteStore;
			}
			else
			{
				localByteStore = UInt32();
				byteStore = localByteStore | 0xFF000000;
				return (byte)(localByteStore >> 24);
			}
		}

		public short Int16()
		{
			return (short)UInt16();
		}

		public ushort UInt16()
		{
			var localShortStore = shortStore;
			if (localShortStore >= 0x100)
			{
				shortStore = localShortStore >> 16;
				return (ushort)localShortStore;
			}
			else
			{
				localShortStore = UInt32();
				shortStore = localShortStore | 0xFFFF0000;
				return (ushort)(localShortStore >> 16);
			}
		}

		public int Int32()
		{
			int index = --this.index;
			if (index < 0)
			{
				Refill();
				index = buffer.Length - 1;
				this.index = index;
			}
			return buffer[index];
		}

		public uint UInt32()
		{
			int index = --this.index;
			if (index < 0)
			{
				Refill();
				index = buffer.Length - 1;
				this.index = index;
			}
			return (uint)buffer[index];
		}

		public Int64 Int64()
		{
			return (long)UInt64();
		}

		public UInt64 UInt64()
		{
			int index = (this.index -= 2);
			if (index < 0)
			{
				Refill();
				index = buffer.Length - 2;
				this.index = index;
			}
			return (ulong)(uint)buffer[index] << 32 | (uint)buffer[index + 1];
		}

		public void Bytes(byte[] data, int start, int count)
		{
			int byteIndex = index * sizeof(int);
			while (count > byteIndex)
			{
				Buffer.BlockCopy(buffer, 0, data, start, byteIndex);
				start += byteIndex;
				Refill();
				byteIndex = buffer.Length * sizeof(int);
			}
			byteIndex -= count;
			Buffer.BlockCopy(buffer, byteIndex, data, start, count);
			index = byteIndex / sizeof(int);
		}

		private static DefaultRandomGen _default = new DefaultRandomGen();

		/// <summary>
		/// Returns a threadsafe global instance of `IRandomGen`.
		/// Due to thread safety it's performance will be lower than if you create your own instance
		/// </summary>
		public static IRandomGen Default
		{
			get
			{
				return _default;
			}
		}

		public int UniformInt(int count)
		{
			return (int)UniformInt((uint)count);
		}

		public uint UniformUInt(uint count)
		{
			Contract.Requires(count > 0);
			Contract.Ensures(Contract.Result<uint>() < count);
			uint max;
			uint rand;
			uint maxUseful;
			do
			{
				if (count <= byte.MaxValue)
				{
					max = byte.MaxValue;
					rand = Byte();
				}
				else if (count <= ushort.MaxValue)
				{
					max = ushort.MaxValue;
					rand = UInt16();
				}
				else
				{
					max = uint.MaxValue;
					rand = UInt32();
				}
				maxUseful = (max / count) * count;
			}
			while (rand > maxUseful);

			return rand % count;
		}

		public ulong UniformUInt(ulong count)
		{
			throw new NotImplementedException();
		}

		public int UniformIntStartEnd(int start, int inclusiveEnd)
		{
			return start + UniformInt(inclusiveEnd - start + 1);
		}

		public int UniformIntStartCount(int start, int count)
		{
			return start + UniformInt(count);
		}

		public long UniformInt(long count)
		{
			throw new NotImplementedException();
		}

		public long UniformIntStartEnd(long start, long inclusiveEnd)
		{
			return start + (long)UniformUInt((ulong)(inclusiveEnd - start + 1));
		}

		public long UniformIntStartCount(long start, long count)
		{
			throw new NotImplementedException();
		}

		public int Binomial(int n, double probability)
		{
			int result = 0;
			for (int i = 0; i < n; i++)
			{
				if (Bool(probability))
					result++;
			}
			return result;
		}

		/// <summary>
		/// Builds on RNGCryptoServiceProvider
		/// </summary>
		public static RandomGen CreateRNGCryptoServiceProvider()
		{
			Contract.Ensures(Contract.Result<RandomGen>() != null);
			return new RandomGen(new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes, 1024 * 8);
		}

		/// <summary>
		/// Based on the Well512 PRNG
		/// Fast, but not secure
		/// </summary>
		public static RandomGen CreateWell512()
		{
			Contract.Ensures(Contract.Result<RandomGen>() != null);
			return new RandomGen(new Well512().GenerateInts, 1024);
		}

		public static RandomGen CreateWell512(long seed)
		{
			Contract.Ensures(Contract.Result<RandomGen>() != null);
			return new RandomGen(new Well512(seed).GenerateInts, 1024);
		}

		/// <summary>
		/// cryptographically secure
		/// </summary>
		/// <returns></returns>
		public static RandomGen CreateSecure()
		{
			Contract.Ensures(Contract.Result<RandomGen>() != null);
			return CreateRNGCryptoServiceProvider();
		}

		/// <summary>
		/// Good enough for most simulation needs
		/// </summary>
		public static RandomGen CreateFast()
		{
			Contract.Ensures(Contract.Result<RandomGen>() != null);
			return CreateWell512();
		}

		public static RandomGen CreateFast(long seed)
		{
			Contract.Ensures(Contract.Result<RandomGen>() != null);
			return CreateWell512(seed);
		}

		/// <summary>
		/// Good enough for all simulation needs
		/// But not cryptographically secure
		/// </summary>
		public static RandomGen Create()
		{
			Contract.Ensures(Contract.Result<RandomGen>() != null);
			return CreateRNGCryptoServiceProvider();
		}

		public static RandomGen Create(long seed)
		{
			throw new NotImplementedException();
		}
	}
}
