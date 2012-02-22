using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Numerics.Histograms
{
	public interface IReadOnlyHistogram<TKey, TValue>
	{
		IHistogramBinning<TKey> Binning { get; }

		TValue ValueByIndex(int binIndex);
		TValue ValueByKey(TKey key);

		TValue TotalWeight { get; }
	}

	public interface IHistogram<TKey, TValue> : IReadOnlyHistogram<TKey, TValue>
	{
		void Add(TKey key);
		void Add(TKey key, TValue weight);
		void AddRange(IEnumerable<TKey> keys);

		void Clear();
		void Clear(IHistogramBinning<TKey> newBinning);
	}
}
