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

		private double cachedGauss;

		public double Uniform()
		{
			ulong value = UInt64();
			//chosen so that result < 1 even when value&mask==mask
			const ulong count = 1ul << 53;
			const ulong mask = count - 1;
			double result = (value & mask) * (1 / (double)count);
			return result;
		}

		public double UniformStartEnd(double start, double exclusiveEnd)
		{
			return (exclusiveEnd - start) * Uniform() + start;
		}

		public double UniformStartLength(double start, double length)
		{
			return length * Uniform() + start;
		}

		public float UniformSingle()
		{
			throw new NotImplementedException();
		}

		public float UniformSingleStartEnd(float start, float exclusiveEnd)
		{
			throw new NotImplementedException();
		}

		public float UniformSingleStartLength(float start, float length)
		{
			throw new NotImplementedException();
		}

		public double Gaussian()
		{
			double oldGauss = this.cachedGauss;
			if (!double.IsNaN(oldGauss))
			{
				this.cachedGauss = double.NaN;
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
				this.cachedGauss = gauss2;
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
			return Byte() > 127;
		}

		public bool Bool(double probability)
		{
			return Uniform() < probability;
		}

		public sbyte SByte()
		{
			return (sbyte)Int32();
		}

		public byte Byte()
		{
			return (byte)Int32();
		}

		public short Int16()
		{
			return (short)Int32();
		}

		public ushort UInt16()
		{
			return (ushort)Int32();
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

		private uint UniformInt(uint count)
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

		public int UniformIntStartEnd(int start, int inclusiveEnd)
		{
			throw new NotImplementedException();
		}

		public int UniformIntStartCount(int start, int count)
		{
			throw new NotImplementedException();
		}

		public long UniformInt(long count)
		{
			throw new NotImplementedException();
		}

		public long UniformIntStartEnd(long start, long inclusiveEnd)
		{
			throw new NotImplementedException();
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
