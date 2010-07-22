using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util.Mathematics
{
	public struct RectangleF
	{
		public Vector2f TopLeft { get; private set; }
		public Vector2f BottomRight { get; private set; }

		public Vector2f Size { get { return BottomRight - TopLeft; } }
		public Vector2f TopRight { get { return new Vector2f(Right, Top); } }
		public Vector2f BottomLeft { get { return new Vector2f(Left, Bottom); } }

		public float Left { get { return TopLeft.X; } }
		public float Top { get { return TopLeft.Y; } }
		public float Right { get { return BottomRight.X; } }
		public float Bottom { get { return BottomRight.Y; } }

		public float Width { get { return Size.X; } }
		public float Height { get { return Size.Y; } }

		/// <summary>
		/// Borders inclusive
		/// </summary>
		public bool Contains(Vector2f v)
		{
			return (v.X >= Left) && (v.Y >= Top) && (v.X <= Right) && (v.Y <= Bottom);
		}

		private RectangleF(Vector2f v1, Vector2f v2)
			: this()
		{
			TopLeft = Vector2f.Min(v1, v2);
			BottomRight = Vector2f.Max(v1, v2);
		}

		public static RectangleF FromLTRB(float x1, float y1, float x2, float y2)
		{
			return FromLTRB(new Vector2f(x1, y1), new Vector2f(x2, y2));
		}

		public static RectangleF FromLTRB(Vector2f v1, Vector2f v2)
		{
			return new RectangleF(v1, v2);
		}

		public static RectangleF FromLTWH(float left, float top, float width, float height)
		{
			return FromLTRB(left, top, left + width, top + height);
		}

		public static RectangleF FromLTWH(Vector2f topleft, Vector2f size)
		{
			return FromLTRB(topleft, topleft + size);
		}
	}
}
