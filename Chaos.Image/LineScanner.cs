using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Chaos.Image
{
	public static class LineScanner
	{
		public static RawColor[] GetRow(this Pixels pix, int y)
		{
			RawColor[] row = new RawColor[pix.Width];
			for (int x = 0; x < row.Length; x++)
				row[x] = pix.Data[x, y];
			return row;
		}

		public static RawColor[] GetColumn(this Pixels pix, int x)
		{
			RawColor[] col = new RawColor[pix.Height];
			for (int y = 0; y < col.Length; y++)
				col[y] = pix.Data[x, y];
			return col;
		}

		public static RawColor[] GetLine(this Pixels pix, Point p1, Point p2)
		{
			int width = p2.X - p1.X;
			int height = p2.Y - p1.Y;
			int longSide = Math.Max(Math.Abs(width), Math.Abs(height));
			double dx = (double)width / longSide;
			double dy = (double)height / longSide;
			RawColor[] result = new RawColor[longSide + 1];
			for (int i = 0; i < longSide + 1; i++)
				result[i] = pix.Data[(int)Math.Round(p1.X + dx * i), (int)Math.Round(p2.Y + dy * i)];
			return result;
		}
	}
}
