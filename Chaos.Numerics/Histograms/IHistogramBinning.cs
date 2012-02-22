using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Chaos.Numerics.Histograms
{
	public interface IHistogramBinning<TNumber>
	{
		[Pure]
		int BinIndexAt(TNumber value);
		[Pure]
		TNumber BinLeft(int binIndex);
		[Pure]
		TNumber BinCenter(int binIndex);

		TNumber Left { get; }
		TNumber Right { get; }
		int Count { get; }
	}

	[ContractClass(typeof(ContractForHistogramBinning))]
	public interface IHistogramBinning : IHistogramBinning<double>
	{
		[Pure]
		int BinIndexAt(double value);
		[Pure]
		double BinLeft(int binIndex);
		[Pure]
		double BinRight(int binIndex);
		[Pure]
		double BinCenter(int binIndex);

		double Left { get; }
		double Right { get; }
		int Count { get; }
	}

	[ContractClassFor(typeof(IHistogramBinning))]
	internal abstract class ContractForHistogramBinning : IHistogramBinning
	{
		public int BinIndexAt(double value)
		{
			Contract.Requires(!double.IsNaN(value));
			Contract.Ensures(Contract.Result<int>() >= -1);
			Contract.Ensures(Contract.Result<int>() <= Count);
			throw new NotImplementedException();
		}

		public double BinLeft(int binIndex)
		{
			Contract.Requires(binIndex >= -1);
			Contract.Requires(binIndex <= Count);
			Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
			Contract.Ensures((binIndex == -1) == double.IsNegativeInfinity(Contract.Result<double>()));
			Contract.Ensures(Contract.Result<double>() < double.PositiveInfinity);
			Contract.Ensures(Contract.Result<double>() <= BinRight(binIndex));
			throw new NotImplementedException();
		}

		public double BinRight(int binIndex)
		{
			throw new NotImplementedException();
		}

		public double BinCenter(int binIndex)
		{
			throw new NotImplementedException();
		}

		public double Left
		{
			get
			{
				Contract.Ensures(Contract.Result<double>() == BinLeft(0));
				throw new NotImplementedException();
			}
		}

		public double Right
		{
			get
			{
				Contract.Ensures(Contract.Result<double>() == BinRight(Count - 1));
				throw new NotImplementedException();
			}
		}

		public int Count
		{
			get
			{
				Contract.Ensures(Contract.Result<int>() >= 0);
				throw new NotImplementedException();
			}
		}
	}
}
