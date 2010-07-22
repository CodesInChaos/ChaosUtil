using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util.Mathematics
{
	public struct Vector2i
	{
		public readonly int X;
		public readonly int Y;

		public Vector2i(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static Vector2i operator +(Vector2i v1, Vector2i v2)
		{
			return new Vector2i(v1.X + v2.X, v1.Y + v2.Y);
		}

		public static Vector2i operator -(Vector2i v1, Vector2i v2)
		{
			return new Vector2i(v1.X - v2.X, v1.Y - v2.Y);
		}

		public static bool operator ==(Vector2i v1, Vector2i v2)
		{
			return (v1.X == v2.X) && (v1.Y == v2.Y);
		}

		public static bool operator !=(Vector2i v1, Vector2i v2)
		{
			return !(v1 == v2);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			return this == (Vector2i)obj;
		}

		// override object.GetHashCode
		public override int GetHashCode()
		{
			return X + Y * 137;
		}

		public override string ToString()
		{
			return "(" + X + "," + Y + ")";
		}

		public static Vector2i Max(Vector2i v1, Vector2i v2)
		{
			return new Vector2i(Math.Max(v1.X, v2.X), Math.Max(v1.Y, v2.Y));
		}

		public static Vector2i Min(Vector2i v1, Vector2i v2)
		{
			return new Vector2i(Math.Min(v1.X, v2.X), Math.Min(v1.Y, v2.Y));
		}

		public static implicit operator Vector2f(Vector2i v)
		{
			return new Vector2f(v.X, v.Y);
		}

		public static Vector2i Zero { get { return new Vector2i(); } }
		public static Vector2i UnitX { get { return new Vector2i(1, 0); } }
		public static Vector2i UnitY { get { return new Vector2i(0, 1); } }
	}
}
