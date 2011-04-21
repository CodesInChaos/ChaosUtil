using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util.Collections
{
	public class MultiDictionary<TKey, TValue> : ILookup<TKey, TValue>, IEnumerable<MultiDictionary<TKey, TValue>.Entry>
	{
		private Dictionary<TKey, List<TValue>> data = new Dictionary<TKey, List<TValue>>();

		public struct Entry : IGrouping<TKey, TValue>, IList<TValue>//ToDo: Make it derive from IList<TValue>
		{
			private readonly MultiDictionary<TKey, TValue> mDictionary;
			private readonly TKey mKey;

			public TKey Key { get { return mKey; } }

			private List<TValue> GetList()
			{
				List<TValue> list;
				mDictionary.data.TryGetValue(Key, out list);
				return list;
			}

			private List<TValue> GetOrCreateList()
			{

				List<TValue> list;
				if (!mDictionary.data.TryGetValue(Key, out list))
				{
					list = new List<TValue>();
					mDictionary.data[Key] = list;
				}
				return list;
			}

			private void FreeIfEmpty(List<TValue> list)
			{
				if (list.Count == 0)
					mDictionary.data.Remove(Key);
			}

			public bool IsEmpty
			{
				get
				{
					return !mDictionary.data.ContainsKey(Key);
				}
			}

			public void Add(TValue value)
			{
				GetOrCreateList().Add(value);
			}

			public bool Remove(TValue value)
			{
				List<TValue> list;
				if (!mDictionary.data.TryGetValue(Key, out list))
					return false;
				bool result = list.Remove(value);
				FreeIfEmpty(list);
				return result;
			}

			public void Clear()
			{
				mDictionary.data.Remove(Key);
			}

			internal Entry(MultiDictionary<TKey, TValue> dictionary, TKey key)
			{
				mDictionary = dictionary;
				mKey = key;
			}

			public IEnumerator<TValue> GetEnumerator()
			{
				List<TValue> list;
				if (!mDictionary.data.TryGetValue(Key, out list))
					return Enumerable.Empty<TValue>().GetEnumerator();
				else
					return list.GetEnumerator();
			}
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public int IndexOf(TValue item)
			{
				List<TValue> list = GetList();
				if (list != null)
					return list.IndexOf(item);
				else
					return -1;
			}

			public void Insert(int index, TValue item)
			{
				GetOrCreateList().Insert(index, item);
			}

			public void RemoveAt(int index)
			{
				List<TValue> list = GetList();
				if (list != null)
				{
					list.RemoveAt(index);
					FreeIfEmpty(list);
				}
				else
					throw new ArgumentOutOfRangeException("index");
			}

			public TValue this[int index]
			{
				get
				{
					return GetList()[index];
				}
				set
				{
					GetList()[index] = value;
				}
			}


			public bool Contains(TValue item)
			{
				var list = GetList();
				if (list != null)
					return list.Contains(item);
				else
					return false;
			}

			public void CopyTo(TValue[] array, int arrayIndex)
			{
				var list = GetList();
				if (list != null)
					list.CopyTo(array, arrayIndex);
			}

			public int Count
			{
				get
				{
					var list = GetList();
					if (list != null)
						return list.Count;
					else
						return 0;
				}
			}

			public bool IsReadOnly
			{
				get { return false; }
			}

			public int RemoveAll(Predicate<TValue> match)
			{
				var list = GetList();
				int result = 0;
				if (list != null)
					result = list.RemoveAll(match);
				FreeIfEmpty(list);
				return result;
			}
		}

		public Entry this[TKey key]
		{
			get
			{
				return new Entry(this, key);
			}
		}

		public bool Contains(TKey key)
		{
			throw new NotImplementedException();
		}

		public int Count
		{
			get { return data.Count; }
		}

		IEnumerable<TValue> ILookup<TKey, TValue>.this[TKey key]
		{
			get { return this[key]; }
		}

		public IEnumerator<Entry> GetEnumerator()
		{
			return new Enumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator<IGrouping<TKey, TValue>> IEnumerable<IGrouping<TKey, TValue>>.GetEnumerator()
		{
			return new Enumerator(this);
		}

		//A mutable struct :(
		public struct Enumerator : IEnumerator<IGrouping<TKey, TValue>>, IEnumerator<Entry>, System.Collections.IEnumerator
		{
			//do not make enumerator readonly! It gets mutated
			//If it were readonly only a copy would be mutated breaking this code
			Dictionary<TKey, List<TValue>>.KeyCollection.Enumerator enumerator;
			readonly MultiDictionary<TKey, TValue> dict;
			IGrouping<TKey, TValue> boxedCurrent;

			public Entry Current
			{
				get { return new Entry(dict, enumerator.Current); }
			}

			public void Dispose()
			{
				boxedCurrent = null;
				enumerator.Dispose();
			}

			object System.Collections.IEnumerator.Current
			{
				get
				{
					if (boxedCurrent == null)
						boxedCurrent = new Entry(dict, enumerator.Current);
					return boxedCurrent;
				}
			}

			public bool MoveNext()
			{
				boxedCurrent = null;
				return enumerator.MoveNext();//Mutates enumerator!
			}

			void System.Collections.IEnumerator.Reset()
			{
				((System.Collections.IEnumerator)enumerator).Reset();
			}

			IGrouping<TKey, TValue> IEnumerator<IGrouping<TKey, TValue>>.Current
			{
				get
				{
					if (boxedCurrent == null)
						boxedCurrent = new Entry(dict, enumerator.Current);
					return boxedCurrent;
				}
			}

			internal Enumerator(MultiDictionary<TKey, TValue> dict)
			{
				this.dict = dict;
				this.enumerator = dict.data.Keys.GetEnumerator();
				this.boxedCurrent = null;
			}
		}
	}
}
