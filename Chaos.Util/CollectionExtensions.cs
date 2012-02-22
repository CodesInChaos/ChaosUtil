using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Collections.Generic
{
	public static class CollectionExtensions
	{
		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue def)
		{
			Contract.Requires(dict != null);
			TValue value;
			if (dict.TryGetValue(key, out value))
				return value;
			else
				return def;
		}

		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
		{
			Contract.Requires(dict != null);
			TValue value;
			dict.TryGetValue(key, out value);
			return value;
		}

		public static IEnumerable<int> Indices<T>(this ICollection<T> list)
		{
			if (list == null)
				throw new ArgumentNullException("list");
			int listCount = list.Count;
			for (int i = 0; i < listCount; i++)
			{
				if (listCount != list.Count)
					throw new InvalidOperationException("list modified");
				yield return i;
			}
			if (listCount != list.Count)
				throw new InvalidOperationException("list modified");
		}

		public static IEnumerable<int> IndicesBackward<T>(this ICollection<T> list)
		{
			if(list==null)
				throw new ArgumentNullException("list");
			for (int i = list.Count; i >= 0; i--)
			{
				yield return i;
			}
		}
	}
}
