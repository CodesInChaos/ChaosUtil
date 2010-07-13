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
		public static Pixels Null { get { return new Pixels(null); } }

		private readonly RawColor[,] mData;

		public int Width { get { return Data.GetLength(0); } }
		public int Height { get { return Data.GetLength(1); } }

		public Size Size { get { return new Size(Width, Height); } }
		public Rectangle Rect { get { return new Rectangle(Point.Empty, Size); } }

		public RawColor[,] Data { get { return mData; } }

		public Pixels(int width, int height)
		{
			mData = new RawColor[width, height];
		}

		public Pixels(RawColor[,] data)
		{
			mData = data;
		}

		public static Pixels CreateFromBitmap(Bitmap bmp)
		{
			Pixels pix = new Pixels(bmp.Width, bmp.Height);
			pix.LoadFromBitmap(bmp);
			return pix;
		}

		public static bool DataEquals(Pixels pix1, Pixels pix2)
		{
			if (pix1.Data == pix2.Data)
				return true;
			if (pix1.Data == null || pix2.Data == null)
				return false;
			if (pix1.Width != pix2.Width || pix1.Height != pix2.Height)
				return false;
			for (int y = 0; y < pix1.Height; y++)
				for (int x = 0; x < pix1.Width; x++)
					if (pix1.Data[x, y] != pix2.Data[x, y])
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
							Data[x, y] = RawColor.FromARGB(*p);
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
