using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Chaos.Image
{
	public class PixelPool
	{
		readonly List<RawColor[]> cache = new List<RawColor[]>();
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

		public Pixels Alloc(int width, int heigth)
		{
			lock (cache)
			{
				mAllocCount++;
				while (cache.Count > 0)
				{
					RawColor[] pix = cache[cache.Count - 1];
					cache.RemoveAt(cache.Count - 1);
					if (pix.Length == heigth * width)
						return new Pixels(pix, width, heigth);
				}
				mCacheMissCount++;
				return new Pixels(width, heigth);
			}
		}

		public void Release(Pixels pix)
		{
			lock (cache)
			{
				cache.Add(pix.Data);
				if (cache.Count > Size)
					cache.RemoveAt(0);
			}
		}

		public PixelPool(int size)
		{
			Size = size;
		}
	}
}
