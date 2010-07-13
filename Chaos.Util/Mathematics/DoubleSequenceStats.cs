using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util
{
	public class DoubleSequenceStats
	{
		public double Count { get; private set; }
		public double Sum { get; private set; }
		public double SumOfSquares { get; private set; }
		public double Average { get { return Sum / Count; } }
		public double Minimum { get; private set; }
		public double Maximum { get; private set; }
		public bool ContainsNaN { get; private set; }
		public double RootMeanSquare { get { return Math.Sqrt(SumOfSquares / Count); } }
		public double Variance
		{
			get
			{
				double average = Average;
				return SumOfSquares - average * average;
			}
		}
		public double StandardDeviation { get { return Math.Sqrt(Variance); } }
		public void Consume(double value)
		{
			Consume(value, 1);
		}

		public void Consume(double value, double weight)
		{
			if (!(weight > 0))
				throw new ArgumentException();
			Count += weight;
			double weightedValue = weight * value;
			Sum += weightedValue;
			SumOfSquares += weightedValue * value;
			if (value < Minimum)
				Minimum = value;
			if (value > Maximum)
				Maximum = value;
			if (Double.IsNaN(value))
				ContainsNaN = true;
		}

		public void Consume(IEnumerable<double> values)
		{
			foreach (double value in values)
				Consume(value);
		}

		public void Consume(IEnumerable<float> values)
		{
			foreach (double value in values)
				Consume(value);
		}

		public DoubleSequenceStats()
		{
			Maximum = Double.NegativeInfinity;
			Minimum = Double.PositiveInfinity;
		}

		public DoubleSequenceStats(IEnumerable<double> values)
			: this()
		{
			Consume(values);
		}

		public DoubleSequenceStats(IEnumerable<float> values)
			: this()
		{
			Consume(values);
		}
	}
}
