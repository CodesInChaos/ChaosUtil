using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util
{
	internal sealed class DefaultRandomGen : IRandomGen
	{
		[ThreadStatic]
		private static RandomGen threadInstance;

		private static RandomGen Instance
		{
			get
			{
				var inst = threadInstance;
				if (inst == null)
				{
					inst = RandomGen.Create();
					threadInstance = inst;
				}
				return inst;
			}
		}

		public double Uniform()
		{
			return Instance.Uniform();
		}

		public double UniformStartEnd(double start, double exclusiveEnd)
		{
			return Instance.UniformStartEnd(start, exclusiveEnd);
		}

		public double UniformStartLength(double start, double length)
		{
			return Instance.UniformStartLength(start, length);
		}

		public float UniformSingle()
		{
			return Instance.UniformSingle();
		}

		public float UniformSingleStartEnd(float start, float exclusiveEnd)
		{
			return Instance.UniformSingleStartEnd(start, exclusiveEnd);
		}

		public float UniformSingleStartLength(float start, float length)
		{
			return Instance.UniformSingleStartLength(start, length);
		}

		public double Gaussian()
		{
			return Instance.Gaussian();
		}

		public double Gaussian(double standardDeviation)
		{
			return Instance.Gaussian(standardDeviation);
		}

		public double Gaussian(double mean, double standardDeviation)
		{
			return Instance.Gaussian(mean, standardDeviation);
		}

		public double Exponential()
		{
			return Instance.Exponential();
		}

		public double ExponentialMean(double mean)
		{
			return Instance.ExponentialMean(mean);
		}

		public double ExponentialRate(double rate)
		{
			return Instance.ExponentialRate(rate);
		}

		public bool Bool()
		{
			return Instance.Bool();
		}

		public bool Bool(double probability)
		{
			return Instance.Bool(probability);
		}

		public sbyte SByte()
		{
			return Instance.SByte();
		}

		public byte Byte()
		{
			return Instance.Byte();
		}

		public short Int16()
		{
			return Instance.Int16();
		}

		public ushort UInt16()
		{
			return Instance.UInt16();
		}

		public int Int32()
		{
			return Instance.Int32();
		}

		public uint UInt32()
		{
			return Instance.UInt32();
		}

		public long Int64()
		{
			return Instance.Int64();
		}

		public ulong UInt64()
		{
			return Instance.UInt64();
		}

		public int UniformInt(int count)
		{
			return Instance.UniformInt(count);
		}

		public int UniformIntStartEnd(int start, int inclusiveEnd)
		{
			return Instance.UniformIntStartEnd(start, inclusiveEnd);
		}

		public int UniformIntStartCount(int start, int count)
		{
			return Instance.UniformIntStartCount(start, count);
		}

		public long UniformInt(long count)
		{
			return Instance.UniformInt(count);
		}

		public long UniformIntStartEnd(long start, long inclusiveEnd)
		{
			return Instance.UniformIntStartEnd(start, inclusiveEnd);
		}

		public long UniformIntStartCount(long start, long count)
		{
			return Instance.UniformIntStartCount(start, count);
		}

		public int Poisson(double mean)
		{
			return Instance.Poisson(mean);
		}

		public int Binomial(int n, double probability)
		{
			return Instance.Binomial(n, probability);
		}

		public void Bytes(byte[] data, int start, int count)
		{
			Instance.Bytes(data, start, count);
		}

		internal DefaultRandomGen()
		{
		}


		public uint UniformUInt(uint count)
		{
			return Instance.UniformUInt(count);
		}

		public ulong UniformUInt(ulong count)
		{
			return Instance.UniformUInt(count);
		}
	}
}
