using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Generic
{
	public static class CollectionExtensions
	{
		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue def)
		{
			TValue value;
			if (dict.TryGetValue(key, out value))
				return value;
			else
				return def;
		}

		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
		{
			TValue value;
			dict.TryGetValue(key, out value);
			return value;
		}
	}
}
