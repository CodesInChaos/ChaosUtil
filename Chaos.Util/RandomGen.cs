using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Chaos.Util
{
	/// <summary>
	/// Delegate that is used to fill the byte array with random data
	/// </summary>
	/// <param name="randomData">the array to fill</param>
	public delegate void RandomBytesProvider(byte[] randomData);

	/// <summary>
	/// The default implementation for IRandomGen
	/// </summary>
	public class RandomGen : IRandomGen
	{
		private readonly RandomBytesProvider provider;

		protected readonly byte[] buffer;
		protected int index;

		protected virtual void Refill()
		{
			provider(buffer);
		}

		protected void Require(int bytes)
		{
			Debug.Assert(bytes >= 0);
			Debug.Assert(bytes <= buffer.Length);
			index -= bytes;
			if (index < 0)
			{
				Refill();
				index = buffer.Length - bytes;
			}
		}

		private double gauss2;

		private double doubleEpsilon;
		private int doubleRandomBytes;
		private float singleEpsilon;
		private int singleRandomBytes;


		public double Uniform()
		{
			const double multiplier = 1.0 / ulong.MaxValue;
			return UInt64() * multiplier;
		}

		public double Gauss()
		{
			if (!double.IsNaN(gauss2))
				return gauss2;
			else
			{
				double u = Uniform();
				double v = Uniform();

				double radius = Math.Sqrt(-2 * Math.Log(u));
				double angle = (2 * Math.PI) * v;

				double gauss1 = radius * Math.Cos(angle);
				gauss2 = radius * Math.Sin(angle);
				return gauss1;
			}
		}

		public double Gauss(double standardDeviation)
		{
			return Gauss() * standardDeviation;
		}

		public double Gauss(double standardDeviation, double mean)
		{
			return Gauss() * standardDeviation + mean;
		}

		public RandomGen(RandomBytesProvider provider, int bufferSize)
		{
			this.provider = provider;
			this.buffer = new byte[bufferSize];
		}

		/// <summary>
		/// Gets an automatically seeded generator
		/// </summary>
		public RandomGen()
			: this(new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes, 2048)
		{

		}

		public double UniformStartEnd(double start, double end)
		{
			return (end - start) * Uniform() + start;
		}

		public double UniformStartLength(double start, double length)
		{
			return (length - start) * Uniform() + start;
		}

		public double Exponential()
		{
			double u = Uniform();
			return Math.Log(u);
		}

		public double Exponential(double mean)
		{
			return mean * Exponential();
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
			Require(1);
			sbyte result = (sbyte)buffer[index];
			return result;
		}

		public byte Byte()
		{
			Require(1);
			byte result = buffer[index];
			return result;
		}

		public short Int16()
		{
			Require(2);
			var result = BitConverter.ToInt16(buffer, index);
			return result;
		}

		public ushort UInt16()
		{
			Require(2);
			var result = BitConverter.ToUInt16(buffer, index);
			return result;
		}

		public int Int32()
		{
			Require(4);
			var result = BitConverter.ToInt32(buffer, index);
			return result;
		}

		public uint UInt32()
		{
			Require(4);
			var result = BitConverter.ToUInt32(buffer, index);
			return result;
		}

		public Int64 Int64()
		{
			Require(8);
			var result = BitConverter.ToInt64(buffer, index);
			return result;
		}

		public UInt64 UInt64()
		{
			Require(8);
			UInt64 result = BitConverter.ToUInt64(buffer, index);
			return result;
		}

		public int UniformInt(int count)
		{
			throw new NotImplementedException();
		}

		public int UniformIntStartEnd(int start, int end)
		{
			return start + UniformInt(end - start);
		}

		public int UniformIntStartCount(int start, int count)
		{
			return start + UniformInt(count);
		}

		public long UniformInt(long count)
		{
			throw new NotImplementedException();
		}

		public long UniformIntStartEnd(long start, long end)
		{
			throw new NotImplementedException();
		}

		public long UniformIntStartCount(long start, long count)
		{
			throw new NotImplementedException();
		}

		public int Binomial(int n, double probability)
		{
			throw new NotImplementedException();
		}

		public long Binomial(long n, double probability)
		{
			throw new NotImplementedException();
		}

		public void Bytes(byte[] data, int start, int count)
		{
			if (start == 0 && count == data.Length)
			{
				provider(data);
			}
			else
			{
				throw new NotImplementedException();
			}
		}
	}

}
