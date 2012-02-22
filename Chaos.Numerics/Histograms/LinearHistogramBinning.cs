using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Numerics.Histograms
{
	internal class LinearHistogramBinning : IHistogramBinning
	{
		readonly double _left;
		readonly double _right;
		readonly double _count;

		public int BinIndexAt(double value)
		{
			throw new NotImplementedException();
		}

		public double BinLeft(int binIndex)
		{
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
			get { throw new NotImplementedException(); }
		}

		public double Right
		{
			get { throw new NotImplementedException(); }
		}

		public int Count
		{
			get { throw new NotImplementedException(); }
		}

		public override string ToString()
		{
			return Left.ToString() + " : " + Right.ToString();
		}
	}
}
