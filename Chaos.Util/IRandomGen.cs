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
		double UniformStartEnd(double start, double exclusiveEnd);
		double UniformStartLength(double start, double length);

		float UniformSingle();
		float UniformSingleStartEnd(float start, float exclusiveEnd);
		float UniformSingleStartLength(float start, float length);

		double Gaussian();
		double Gaussian(double standardDeviation);
		double Gaussian(double mean, double standardDeviation);

		double Exponential();
		double ExponentialMean(double mean);
		double ExponentialRate(double rate);

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
		int UniformIntStartEnd(int start, int inclusiveEnd);
		int UniformIntStartCount(int start, int count);

		long UniformInt(long count);
		long UniformIntStartEnd(long start, long inclusiveEnd);
		long UniformIntStartCount(long start, long count);

		int Poisson(double mean);
		int Binomial(int n, double probability);

		void Bytes(byte[] data, int start, int count);
	}

	[ContractClassFor(typeof(IRandomGen))]
	internal abstract class ContractForIRandomGen : IRandomGen
	{
		double IRandomGen.Uniform()
		{
			Contract.Ensures(Contract.Result<double>() >= 0);
			Contract.Ensures(Contract.Result<double>() < 1);
			throw new NotImplementedException();
		}

		double IRandomGen.UniformStartEnd(double start, double exclusiveEnd)
		{
			Contract.Requires(start < exclusiveEnd);
			Contract.Requires(start > double.NegativeInfinity);
			Contract.Requires(exclusiveEnd < double.PositiveInfinity);
			Contract.Ensures(Contract.Result<double>() >= start);
			Contract.Ensures(Contract.Result<double>() < exclusiveEnd);
			throw new NotImplementedException();
		}

		double IRandomGen.UniformStartLength(double start, double length)
		{
			Contract.Requires(length > 0);
			Contract.Requires(start > double.NegativeInfinity);
			Contract.Requires(length < double.PositiveInfinity);
			Contract.Ensures(Contract.Result<double>() >= start);
			Contract.Ensures(Contract.Result<double>() < start + length);
			throw new NotImplementedException();
		}

		float IRandomGen.UniformSingle()
		{
			Contract.Ensures(Contract.Result<float>() >= 0);
			Contract.Ensures(Contract.Result<float>() < 1);
			throw new NotImplementedException();
		}

		float IRandomGen.UniformSingleStartEnd(float start, float exclusiveEnd)
		{
			Contract.Requires(start < exclusiveEnd);
			Contract.Requires(start > float.NegativeInfinity);
			Contract.Requires(exclusiveEnd < float.PositiveInfinity);
			Contract.Ensures(Contract.Result<float>() >= start);
			Contract.Ensures(Contract.Result<float>() < exclusiveEnd);
			throw new NotImplementedException();
		}

		float IRandomGen.UniformSingleStartLength(float start, float length)
		{
			Contract.Requires(length > 0);
			Contract.Requires(start > float.NegativeInfinity);
			Contract.Requires(length < float.PositiveInfinity);
			Contract.Ensures(Contract.Result<float>() >= start);
			Contract.Ensures(Contract.Result<float>() < start + length);
			throw new NotImplementedException();
		}

		double IRandomGen.Gaussian()
		{
			throw new NotImplementedException();
		}

		double IRandomGen.Gaussian(double standardDeviation)
		{
			Contract.Requires(standardDeviation >= 0);
			Contract.Requires(standardDeviation < double.PositiveInfinity);
			throw new NotImplementedException();
		}

		double IRandomGen.Gaussian(double mean, double standardDeviation)
		{
			Contract.Requires(mean > double.NegativeInfinity);
			Contract.Requires(mean < double.PositiveInfinity);
			Contract.Requires(standardDeviation >= 0);
			Contract.Requires(standardDeviation < double.PositiveInfinity);
			throw new NotImplementedException();
		}

		double IRandomGen.Exponential()
		{
			Contract.Ensures(Contract.Result<double>() >= 0);
			throw new NotImplementedException();
		}

		double IRandomGen.ExponentialMean(double mean)
		{
			Contract.Requires(mean >= 0);
			Contract.Ensures(Contract.Result<double>() >= 0);
			throw new NotImplementedException();
		}

		double IRandomGen.ExponentialRate(double rate)
		{
			Contract.Requires(rate >= 0);
			Contract.Ensures(Contract.Result<double>() >= 0);
			throw new NotImplementedException();
		}

		int IRandomGen.Poisson(double mean)
		{
			Contract.Requires(mean >= 0);
			Contract.Ensures(Contract.Result<int>() >= 0);
			throw new NotImplementedException();
		}

		bool IRandomGen.Bool()
		{
			throw new NotImplementedException();
		}

		bool IRandomGen.Bool(double probability)
		{
			Contract.Requires(probability >= 0);
			Contract.Requires(probability <= 1);
			throw new NotImplementedException();
		}

		sbyte IRandomGen.SByte()
		{
			throw new NotImplementedException();
		}

		byte IRandomGen.Byte()
		{
			throw new NotImplementedException();
		}

		short IRandomGen.Int16()
		{
			throw new NotImplementedException();
		}

		ushort IRandomGen.UInt16()
		{
			throw new NotImplementedException();
		}

		int IRandomGen.Int32()
		{
			throw new NotImplementedException();
		}

		uint IRandomGen.UInt32()
		{
			throw new NotImplementedException();
		}

		long IRandomGen.Int64()
		{
			throw new NotImplementedException();
		}

		ulong IRandomGen.UInt64()
		{
			throw new NotImplementedException();
		}

		int IRandomGen.UniformInt(int count)
		{
			Contract.Requires(count >= 1);
			Contract.Ensures(Contract.Result<int>() >= 0);
			Contract.Ensures(Contract.Result<int>() < count);
			throw new NotImplementedException();
		}

		int IRandomGen.UniformIntStartEnd(int start, int inclusiveEnd)
		{
			Contract.Requires(inclusiveEnd >= start);
			Contract.Ensures(Contract.Result<int>() >= start);
			Contract.Ensures(Contract.Result<int>() <= inclusiveEnd);
			throw new NotImplementedException();
		}

		int IRandomGen.UniformIntStartCount(int start, int count)
		{
			Contract.Requires(count >= 1);
			Contract.Requires(start + count >= start);//No int overflow
			Contract.Ensures(Contract.Result<int>() >= start);
			Contract.Ensures(Contract.Result<int>() < start + count);
			throw new NotImplementedException();
		}

		long IRandomGen.UniformInt(long count)
		{
			Contract.Requires(count >= 1);
			Contract.Ensures(Contract.Result<long>() >= 0);
			Contract.Ensures(Contract.Result<long>() < count);
			throw new NotImplementedException();
		}

		long IRandomGen.UniformIntStartEnd(long start, long inclusiveEnd)
		{
			Contract.Requires(inclusiveEnd >= start);
			Contract.Ensures(Contract.Result<long>() >= start);
			Contract.Ensures(Contract.Result<long>() <= inclusiveEnd);
			throw new NotImplementedException();
		}

		long IRandomGen.UniformIntStartCount(long start, long count)
		{
			Contract.Requires(count >= 1);
			Contract.Requires(start + count >= start);//No int overflow
			Contract.Ensures(Contract.Result<long>() >= start);
			Contract.Ensures(Contract.Result<long>() < start + count);
			throw new NotImplementedException();
		}

		int IRandomGen.Binomial(int n, double probability)
		{
			Contract.Requires(n >= 0);
			Contract.Requires(probability >= 0);
			Contract.Requires(probability <= 1);
			Contract.Ensures(Contract.Result<int>() >= 0);
			Contract.Ensures(Contract.Result<int>() <= n);
			throw new NotImplementedException();
		}

		void IRandomGen.Bytes(byte[] data, int start, int count)
		{
			Contract.Requires(data != null);
			Contract.Requires(start >= 0);
			Contract.Requires(count >= 0);
			Contract.Requires(start + count <= data.Length);
			Contract.Requires(start + count >= start);//No int overflow
			throw new NotImplementedException();
		}
	}
}
