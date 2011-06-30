using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Chaos.Image
{
	public struct Pixels
	{
		public static Pixels Null { get { return new Pixels(); } }

		private readonly int[] mData;
		private readonly int width;
		private readonly int height;
		private readonly int stride;
		private readonly int start;
		//private unsafe int* pData;

		public int Width { get { return width; } }
		public int Height { get { return height; } }

		//public Size Size { get { return new Size(Width, Height); } }
		//public Rectangle Rect { get { return new Rectangle(Point.Empty, Size); } }

		public int[] Data { get { return mData; } }

		public Pixels(int width, int height)
			: this(new int[width * height], width, height, 0, height)
		{
		}

		internal Pixels(int[] data, int width, int height, int start, int stride)
		{
			if (data.Length < (long)width * (long)height)
				throw new ArgumentException("Dimensions don't fit array");
			mData = data;
			this.width = width;
			this.height = height;
			this.start = start;
			this.stride = stride;
		}

		public RawColor this[int x, int y]
		{
			get
			{
				return RawColor.FromARGB(mData[start + x + width * y]);
			}
			set
			{
				mData[x + width * y] = (int)value.ARGB;
			}
		}

		public static Pixels CreateFromBitmap(Bitmap bmp)
		{
			Pixels pix = new Pixels(bmp.Width, bmp.Height);
			pix.LoadFromBitmap(bmp);
			return pix;
		}

		public static Pixels CreateFromBitmap(Bitmap bmp, PixelPool pool)
		{
			Pixels pix = pool.Alloc(bmp.Width, bmp.Height);
			pix.LoadFromBitmap(bmp);
			return pix;
		}

		public static bool DataEquals(Pixels pix1, Pixels pix2)
		{
			if (pix1.Width != pix2.Width || pix1.Height != pix2.Height)
				return false;
			if (pix1.Data == pix2.Data)
				return true;
			if (pix1.Data == null || pix2.Data == null)
				return false;
			int len = pix1.Width * pix1.Height;
			for (int i = 0; i < len; i++)
				if (pix1.Data[i] != pix2.Data[i])
					return false;
			return true;
		}

		public void LoadFromBitmap(Bitmap bmp)
		{
			if (bmp.Width != Width || bmp.Height != Height)
				throw new ArgumentException("Size missmatch");
			unsafe
			{
				BitmapData bmpData = null;
				try
				{
					bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

					for (int y = 0; y < bmpData.Height; y++)
					{
						uint* p = (uint*)((byte*)bmpData.Scan0 + y * bmpData.Stride);
						for (int x = 0; x < bmpData.Width; x++)
						{
							this[x, y] = RawColor.FromARGB(*p);
							p++;
						}
					}
				}
				finally
				{
					if (bmpData != null)
						bmp.UnlockBits(bmpData);
				}
			}
		}
	}
}
