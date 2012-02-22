using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Chaos.Numerics.Histograms
{
	public static class HistogramBinning
	{
		public static IHistogramBinning CreateLinear(double left, double right, int count)
		{
			Contract.Requires(count > 0);
			Contract.Requires(left >= double.NegativeInfinity);
			Contract.Requires(right <= double.PositiveInfinity);
			Contract.Requires(left <= right);
			throw new NotImplementedException();
		}
	}
}
