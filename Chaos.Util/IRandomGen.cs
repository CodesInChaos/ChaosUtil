using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace Chaos.Util
{
	[ContractClass(typeof(ContractForIRandomGen))]
	public interface IRandomGen
	{
		double Uniform();
		double UniformStartEnd(double start, double end);
		double UniformStartLength(double start, double length);

		double Gauss();
		double Gauss(double standardDeviation);
		double Gauss(double mean, double standardDeviation);

		double Exponential();
		double Exponential(double mean);

		int Poisson(double mean);

		bool Bool();
		bool Bool(double probability);

		SByte SByte();
		Byte Byte();
		Int16 Int16();
		UInt16 UInt16();
		Int32 Int32();
		UInt32 UInt32();
		Int64 Int64();
		UInt64 UInt64();

		int UniformInt(int count);
		//start and end inclusive
		int UniformIntStartEnd(int start, int inclusiveEnd);
		int UniformIntStartCount(int start, int count);
		long UniformInt(long count);
		long UniformIntStartEnd(long start, long inclusiveEnd);
		long UniformIntStartCount(long start, long count);


		int Binomial(int n, double probability);
		long Binomial(long n, double probability);

		void Bytes(byte[] data, int start, int count);
	}

	[ContractClassFor(typeof(IRandomGen))]
	internal abstract class ContractForIRandomGen : IRandomGen
	{
		public double Uniform()
		{
			Contract.Ensures(Contract.Result<double>() >= 0);
			Contract.Ensures(Contract.Result<double>() <= 1);
			throw new NotImplementedException();
		}

		public double UniformStartEnd(double start, double end)
		{
			Contract.Requires(start <= end);
			Contract.Ensures(Contract.Result<double>() >= start);
			Contract.Ensures(Contract.Result<double>() <= end);
			throw new NotImplementedException();
		}

		public double UniformStartLength(double start, double length)
		{
			Contract.Requires(length >= 0);
			Contract.Ensures(Contract.Result<double>() >= start);
			Contract.Ensures(Contract.Result<double>() <= length);
			throw new NotImplementedException();
		}

		public double Gauss()
		{
			throw new NotImplementedException();
		}

		public double Gauss(double standardDeviation)
		{
			Contract.Requires(standardDeviation >= 0);
			throw new NotImplementedException();
		}

		public double Gauss(double mean, double standardDeviation)
		{
			Contract.Requires(standardDeviation >= 0);
			throw new NotImplementedException();
		}

		public double Exponential()
		{
			Contract.Ensures(Contract.Result<double>() >= 0);
			throw new NotImplementedException();
		}

		public double Exponential(double mean)
		{
			Contract.Requires(mean >= 0);
			Contract.Ensures(Contract.Result<double>() >= 0);
			throw new NotImplementedException();
		}

		public int Poisson(double mean)
		{
			Contract.Requires(mean >= 0);
			Contract.Ensures(Contract.Result<int>() >= 0);
			throw new NotImplementedException();
		}

		public bool Bool()
		{
			throw new NotImplementedException();
		}

		public bool Bool(double probability)
		{
			Contract.Requires(probability >= 0);
			Contract.Requires(probability <= 1);
			throw new NotImplementedException();
		}

		public sbyte SByte()
		{
			throw new NotImplementedException();
		}

		public byte Byte()
		{
			throw new NotImplementedException();
		}

		public short Int16()
		{
			throw new NotImplementedException();
		}

		public ushort UInt16()
		{
			throw new NotImplementedException();
		}

		public int Int32()
		{
			throw new NotImplementedException();
		}

		public uint UInt32()
		{
			throw new NotImplementedException();
		}

		public long Int64()
		{
			throw new NotImplementedException();
		}

		public ulong UInt64()
		{
			throw new NotImplementedException();
		}

		public int UniformInt(int count)
		{
			Contract.Requires(count >= 1);
			Contract.Ensures(Contract.Result<int>() >= 0);
			Contract.Ensures(Contract.Result<int>() < count);
			throw new NotImplementedException();
		}

		public int UniformIntStartEnd(int start, int inclusiveEnd)
		{
			Contract.Requires(inclusiveEnd >= start);
			Contract.Ensures(Contract.Result<int>() >= start);
			Contract.Ensures(Contract.Result<int>() <= inclusiveEnd);
			throw new NotImplementedException();
		}

		public int UniformIntStartCount(int start, int count)
		{
			Contract.Requires(count >= 1);
			Contract.Requires(start + count >= 0);
			Contract.Ensures(Contract.Result<int>() >= start);
			Contract.Ensures(Contract.Result<int>() < start + count);
			throw new NotImplementedException();
		}

		public long UniformInt(long count)
		{
			Contract.Requires(count >= 1);
			Contract.Ensures(Contract.Result<long>() >= 0);
			Contract.Ensures(Contract.Result<long>() < count);
			throw new NotImplementedException();
		}

		public long UniformIntStartEnd(long start, long inclusiveEnd)
		{
			Contract.Requires(inclusiveEnd >= start);
			Contract.Ensures(Contract.Result<long>() >= start);
			Contract.Ensures(Contract.Result<long>() <= inclusiveEnd);
			throw new NotImplementedException();
		}

		public long UniformIntStartCount(long start, long count)
		{
			Contract.Requires(count >= 1);
			Contract.Ensures(Contract.Result<int>() >= start);
			Contract.Ensures(Contract.Result<int>() < start + count);
			throw new NotImplementedException();
		}

		public int Binomial(int n, double probability)
		{
			Contract.Requires(n >= 0);
			Contract.Requires(probability >= 0);
			Contract.Requires(probability <= 1);
			Contract.Ensures(Contract.Result<int>() >= 0);
			Contract.Ensures(Contract.Result<int>() <= n);
			throw new NotImplementedException();
		}

		public long Binomial(long n, double probability)
		{
			Contract.Requires(n >= 0);
			Contract.Requires(probability >= 0);
			Contract.Requires(probability <= 1);
			Contract.Ensures(Contract.Result<long>() >= 0);
			Contract.Ensures(Contract.Result<long>() <= n);
			throw new NotImplementedException();
		}

		public void Bytes(byte[] data, int start, int count)
		{
			Contract.Requires(data != null);
			Contract.Requires(start >= 0);
			Contract.Requires(count >= 0);
			Contract.Requires(start + count <= data.Length);
			throw new NotImplementedException();
		}
	}
}
