using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util.Mathematics
{
	public struct RectangleI
	{

		public Vector2i TopLeft { get; private set; }
		public Vector2i BottomRight { get; private set; }

		public Vector2i Size { get { return BottomRight - TopLeft; } }
		public Vector2i TopRight { get { return new Vector2i(Right, Top); } }
		public Vector2i BottomLeft { get { return new Vector2i(Left, Bottom); } }

		public int Left { get { return TopLeft.X; } }
		public int Top { get { return TopLeft.Y; } }
		public int Right { get { return BottomRight.X; } }
		public int Bottom { get { return BottomRight.Y; } }

		public int Width { get { return Size.X; } }
		public int Height { get { return Size.Y; } }

		/// <summary>
		/// Borders inclusive
		/// </summary>
		public bool Contains(Vector2i v)
		{
			return (v.X >= Left) && (v.Y >= Top) && (v.X <= Right) && (v.Y <= Bottom);
		}

		private RectangleI(Vector2i v1, Vector2i v2)
			: this()
		{
			TopLeft = Vector2i.Min(v1, v2);
			BottomRight = Vector2i.Max(v1, v2);
		}

		public static implicit operator RectangleF(RectangleI rect)
		{
			return RectangleF.FromLTRB(rect.TopLeft, rect.BottomRight);
		}

		public static RectangleI FromLTRB(int x1, int y1, int x2, int y2)
		{
			return FromLTRB(new Vector2i(x1, y1), new Vector2i(x2, y2));
		}

		public static RectangleI FromLTRB(Vector2i v1, Vector2i v2)
		{
			return new RectangleI(v1, v2);
		}

		public static RectangleI FromLTWH(int left, int top, int width, int height)
		{
			return FromLTRB(left, top, left + width, top + height);
		}

		public static RectangleI FromLTWH(Vector2i topleft, Vector2i size)
		{
			return FromLTRB(topleft, topleft + size);
		}
	}
}
