using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util
{
	/*
	public class ImmutableList<T>:IEnumerable<T>
	{
		private T[] data;
		private int count;
		private bool exclusive;

		public ImmutableList<T> Truncate(int newCount)
		{
			exclusive = false;
			return new ImmutableList<T>(data, newCount, false);
		}

		public int IndexOf(T item)
		{
			throw new NotImplementedException();
		}

		public ImmutableList<T> Insert(int index, T item)
		{
			throw new NotImplementedException();
		}

		public ImmutableList<T> RemoveAt(int index)
		{
			if (((UInt32)index) < count - 1)
			{

			}
			else if (index == count - 1)//Last Element
			{
				return new ImmutableList<T>(data, count - 1);
			}
			else
				throw new IndexOutOfRangeException();
		}

		public T this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public ImmutableList<T> Add(T item)
		{
			throw new NotImplementedException();
		}

		public static readonly ImmutableList<T> Empty;

		public bool Contains(T item)
		{
			for (int i = 0; i < count; i++)
			{
				if (data[i] == item)
					return true;
			}
			return false;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(data, 0, array, arrayIndex, count);
		}

		public int Count
		{
			get { return count; }
		}

		public bool IsReadOnly
		{
			get { return true; }
		}

		public ImmutableList<T> Remove(T item)
		{
			int i = IndexOf(T);
			if (i < 0)
				return null;
			else
				return RemoveAt();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return data.Take(count);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return (System.Collections.IEnumerator)GetEnumerator();
		}

		public static ImmutableList<T> operator +(ImmutableList<T> left, ImmutableList<T> right)
		{
			if (right.Count == 0)
				return left;
			else if (right.Count == 1)
				return left.Add(right[0]);
			else
			{
				T[] data = new T[arraySize(left.count + right.count)];
			}
		}

		private static object arraySize(int count)
		{
			throw new NotImplementedException();
		}

		public static ImmutableList<T> operator +(ImmutableList<T> left, T right)
		{
			return left.Add(right);
		}

		private ImmutableList(T[] internalArray, int count, bool exclusive)
		{
			data = internalArray;
			this.count = count;
			this.exclusive = exclusive;
		}

		public ImmutableList(IEnumerable<T> elements)
		{
			data = elements.ToArray();
			count = data.Length;
			exclusive = true;
		}

		public ImmutableList(params T[] elements)
		{
			data = new T[elements.Length];
			count = elements.Length;
			elements.CopyTo(data, 0);
		}
	}
	*/
}
