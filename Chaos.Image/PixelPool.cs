using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Chaos.Image
{
	public class PixelPool
	{
		readonly List<int[]> cache = new List<int[]>();
		public readonly int Size;
		int mAllocCount = 0;
		int mCacheMissCount = 0;

		public int AllocCount
		{
			get
			{
				lock (cache)
				{
					return mAllocCount;
				}
			}
		}

		public int CacheMissCount
		{
			get
			{
				lock (cache)
				{
					return mCacheMissCount;
				}
			}
		}

		public Pixels Alloc(int width, int height)
		{
			return new Pixels(Alloc(width * height), width, height, 0, height);
		}

		private int[] Alloc(int size)
		{
			lock (cache)
			{
				mAllocCount++;
				while (cache.Count > 0)
				{
					int[] pix = cache[cache.Count - 1];
					cache.RemoveAt(cache.Count - 1);
					if (pix.Length == size)
						return pix;
				}
				mCacheMissCount++;
				return new int[size];
			}
		}

		private void Release(int[] pix)
		{
			lock (cache)
			{
				cache.Add(pix);
				if (cache.Count > Size)
					cache.RemoveAt(0);
			}
		}

		public void Release(Pixels pix)
		{
			Release(pix.Data);
		}

		public PixelPool(int size)
		{
			Size = size;
		}
	}
}
