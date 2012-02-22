using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Chaos.Util
{
	public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		private readonly IDictionary<TKey, TValue> _dict;

		[ContractInvariantMethod]
		void ObjectInvariant()
		{
			Contract.Invariant(_dict != null);
		}

		public static ReadOnlyDictionary<TKey, TValue> CreateIndependent(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Contract.Requires(items != null);
			var dict = new Dictionary<TKey, TValue>();
			foreach (var item in items)
				dict.Add(item.Key, item.Value);
			return new ReadOnlyDictionary<TKey, TValue>(dict);
		}

		public ReadOnlyDictionary(IDictionary<TKey, TValue> dict)
		{
			Contract.Requires(dict != null);
			_dict = dict;
		}

		public bool ContainsKey(TKey key)
		{
			return _dict.ContainsKey(key);
		}

		public ICollection<TKey> Keys
		{
			get { return _dict.Keys; }
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return _dict.TryGetValue(key, out value);
		}

		public ICollection<TValue> Values
		{
			get { return _dict.Values; }
		}

		public TValue this[TKey key]
		{
			get
			{
				return _dict[key];
			}
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return _dict.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			_dict.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return _dict.Count; }
		}

		public bool IsReadOnly
		{
			get { return true; }
		}


		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return _dict.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		//Useless stuff

		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		TValue IDictionary<TKey, TValue>.this[TKey key]
		{
			get
			{
				return this[key];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			throw new NotSupportedException();
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}
	}
}
