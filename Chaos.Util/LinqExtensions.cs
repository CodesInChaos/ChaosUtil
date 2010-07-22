using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chaos.Util;
using Chaos.Util.Mathematics;

namespace System.Linq
{
	public static class LinqExtensions
	{
		public static int? FirstIndex<T>(this IEnumerable<T> source, Predicate<T> predicate)
		{
			int i = 0;
			foreach (T element in source)
			{
				if (predicate(element))
					return i;
				i++;
			}
			return null;
		}

		public static int? FirstIndex<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
		{
			int i = 0;
			foreach (T element in source)
			{
				if (predicate(element, i))
					return i;
				i++;
			}
			return null;
		}

		public static int? LastIndex<T>(this IEnumerable<T> source, Predicate<T> predicate)
		{
			int i = 0;
			int? last = null;
			foreach (T element in source)
			{
				if (predicate(element))
					last = i;
				i++;
			}
			return last;
		}

		public static int? LastIndex<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
		{
			int i = 0;
			int? last = null;
			foreach (T element in source)
			{
				if (predicate(element, i))
					last = i;
				i++;
			}
			return last;
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
			if (enumerator.MoveNext())
				return enumerator.Current;
			else
				return defaultValue;
		}

		public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue)
		{
			IEnumerator<T> enumerator = source.Where(predicate).GetEnumerator();
			if (enumerator.MoveNext())
				return enumerator.Current;
			else
				return defaultValue;
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

		public static IEnumerable<T> DepthFirstSearch<T>(T root)
			where T : IEnumerable<T>
		{
			return DepthFirstSearch(root, x => x);
		}

		public static IEnumerable<T> DepthFirstSearch<T>(T root, Func<T, IEnumerable<T>> getChildren)
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
