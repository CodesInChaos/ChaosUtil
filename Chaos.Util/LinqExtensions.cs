using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chaos.Util;
using Chaos.Util.Mathematics;
using System.Diagnostics.Contracts;

namespace System.Linq
{
	public static class LinqExtensions
	{
		// Returns the element of source for which valueFunc is maximal
		// If there are several maxima the first one is returned
		// NaNs are never considered as maximum.
		// If the sequence is empty or consists only of NaNs the result is `defaultValue`
		public static T ArgMaxOrDefault<T>(this IEnumerable<T> source, Func<T, double> valueFunc, T defaultValue)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(valueFunc != null);
			T result = defaultValue;
			double max = 0;
			bool first = true;
			foreach (T elem in source)
			{
				double value = valueFunc(elem);
				if (double.IsNaN(value))
					continue;
				if (first || value > max)
				{
					max = value;
					result = elem;
					first = false;
				}
			}
			return result;
		}

		public static T ArgMaxOrDefault<T>(this IEnumerable<T> source, Func<T, double> valueFunc)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(valueFunc != null);
			return ArgMaxOrDefault(source, valueFunc, default(T));
		}

		//Return -1 if no element satisfies the predicate
		public static int FirstIndex<T>(this IEnumerable<T> source, Predicate<T> predicate)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(predicate != null);
			Contract.Ensures(Contract.Result<int>() >= -1);
			int i = 0;
			foreach (T element in source)
			{
				if (predicate(element))
					return i;
				i++;
			}
			return -1;
		}

		//Return -1 if no element satisfies the predicate
		public static int FirstIndex<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(predicate != null);
			Contract.Ensures(Contract.Result<int>() >= -1);
			int i = 0;
			foreach (T element in source)
			{
				if (predicate(element, i))
					return i;
				i++;
			}
			return -1;
		}

		//Return -1 if no element satisfies the predicate
		public static int LastIndex<T>(this IEnumerable<T> source, Predicate<T> predicate)
		{
			return LastIndex(source, (elem, i) => predicate(elem));
		}

		public static int LastIndex<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(predicate != null);
			Contract.Ensures(Contract.Result<int>() >= -1);

			IList<T> sourceList = source as IList<T>;
			if (sourceList != null)
			{
				for (int i = sourceList.Count; i >= 0; i--)
				{
					if (predicate(sourceList[i], i))
						return i;
				}
				return -1;
			}
			else
			{
				int i = 0;
				int last = -1;
				foreach (T element in source)
				{
					if (predicate(element, i))
						last = i;
					i++;
				}
				return last;
			}
		}

		public static IEnumerable<T> TakeWhileIncludingTerminator<T>(this IEnumerable<T> source, Predicate<T> predecate)
		{
			foreach (T element in source)
			{
				yield return element;
				if (predecate(element))
					yield break;
			}
		}

		public static T? FirstOrNull<T>(this IEnumerable<T> source)
			where T : struct
		{
			return source.Select(element => (T?)element).FirstOrDefault();
		}

		public static T? FirstOrNull<T>(this IEnumerable<T> source, Func<T, bool> predicate)
			where T : struct
		{
			return source.Where(predicate).Select(element => (T?)element).FirstOrDefault();
		}

		public static T FirstOrDefault<T>(this IEnumerable<T> source, T defaultValue)
		{
			IEnumerator<T> enumerator = source.GetEnumerator();
			using (enumerator)
			{
				if (enumerator.MoveNext())
					return enumerator.Current;
				else
					return defaultValue;
			}
		}

		public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue)
		{
			IEnumerator<T> enumerator = source.Where(predicate).GetEnumerator();
			using (enumerator)
			{
				if (enumerator.MoveNext())
					return enumerator.Current;
				else
					return defaultValue;
			}
		}


		public static string ConcatToString(this IEnumerable<String> strings, string seperator)
		{
			StringBuilder sb = new StringBuilder();
			bool first = true;
			foreach (string s in strings)
			{
				if (first)
					first = false;
				else
					sb.Append(seperator);
				sb.Append(s);
			}
			return sb.ToString();
		}

		public static string ConcatToString(this IEnumerable<String> strings)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in strings)
			{
				sb.Append(s);
			}
			return sb.ToString();
		}

		public static DoubleSequenceStats SequenceStats(this IEnumerable<double> values)
		{
			return new DoubleSequenceStats(values);
		}

		public static DoubleSequenceStats SequenceStats(this IEnumerable<float> values)
		{
			return new DoubleSequenceStats(values);
		}

		public static IEnumerable<T> DepthFirstSearch<T>(this T root)
			where T : IEnumerable<T>
		{
			return DepthFirstSearch(root, x => x);
		}

		public static IEnumerable<T> DepthFirstSearch<T>(this T root, Func<T, IEnumerable<T>> getChildren)
		{
			yield return root;
			foreach (T child in getChildren(root))
			{
				foreach (T descendant in DepthFirstSearch(child, getChildren))
					yield return descendant;
			}
		}
	}
}
