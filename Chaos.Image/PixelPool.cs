using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Chaos.Image
{
	public class PixelPool
	{
		readonly List<RawColor[,]> cache = new List<RawColor[,]>();
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

		public RawColor[,] Alloc(int width, int heigth)
		{
			lock (cache)
			{
				mAllocCount++;
				while (cache.Count > 0)
				{
					RawColor[,] pix = cache[cache.Count - 1];
					cache.RemoveAt(cache.Count - 1);
					if (pix.GetLength(0) == width && pix.GetLength(1) == heigth)
						return pix;
				}
				mCacheMissCount++;
				return new RawColor[width, heigth];
			}
		}

		public void Release(RawColor[,] pix)
		{
			lock (cache)
			{
				cache.Add(pix);
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
