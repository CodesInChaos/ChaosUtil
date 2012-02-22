using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util.Mathematics
{
	public struct Vector2f : IEquatable<Vector2f>, IFormattable
	{
		private readonly float x;
		private readonly float y;

		public float X { get { return x; } }
		public float Y { get { return y; } }

		public Vector2f(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public static Vector2f Zero { get { return new Vector2f(); } }
		public static Vector2f UnitX { get { return new Vector2f(1, 0); } }
		public static Vector2f UnitY { get { return new Vector2f(0, 1); } }
		public static Vector2f One { get { return new Vector2f(1, 1); } }

		public static float Distance(Vector2f v1, Vector2f v2)
		{
			return (v2 - v1).Length;
		}

		public static float DistanceSquared(Vector2f v1, Vector2f v2)
		{
			return (v2 - v1).LengthSquared;
		}

		public static float Dot(Vector2f v1, Vector2f v2)
		{
			return v1.X * v2.X + v1.Y * v2.Y;
		}

		public static Vector2f operator -(Vector2f v)
		{
			return new Vector2f(-v.X, -v.Y);
		}

		public static Vector2f operator +(Vector2f l, Vector2f r)
		{
			return new Vector2f(l.X + r.X, l.Y + r.Y);
		}

		public static Vector2f operator -(Vector2f l, Vector2f r)
		{
			return new Vector2f(l.X - r.X, l.Y - r.Y);
		}

		public static Vector2f operator *(Vector2f vL, Vector2f vR)
		{
			return new Vector2f(vL.X * vR.X, vL.Y * vR.Y);
		}

		public static Vector2f operator *(Vector2f v, float s)
		{
			return new Vector2f(v.X * s, v.Y * s);
		}

		public static Vector2f operator /(Vector2f vL, Vector2f vR)
		{
			return new Vector2f(vL.X / vR.X, vL.Y / vR.Y);
		}

		public static Vector2f operator *(float s, Vector2f v)
		{
			return new Vector2f(v.X * s, v.Y * s);
		}

		public static Vector2f operator /(Vector2f v, float s)
		{
			return new Vector2f(v.X / s, v.Y / s);
		}

		public float Length { get { return (float)Math.Sqrt(X * X + Y * Y); } }
		public float LengthSquared { get { return X * X + Y * Y; } }
		public Vector2f Normalized { get { return this / Length; } }

		public Vector2f Rotate(float angle)
		{
			float s = (float)Math.Sin(angle);
			float c = (float)Math.Cos(angle);
			return new Vector2f(X * c - Y * s, Y * c + s * X);
		}

		public float AngleTo()
		{
			return (float)Math.Atan2(Y, X);
		}

		public Vector2f Clamp(float maxLength)
		{
			float len = Length;
			if (len > maxLength)
				return this * (maxLength / len);
			else
				return this;
		}

		public Vector2f Clamp(float minLength, float maxLength)
		{
			float len = Length;
			if (len < minLength)
				return this * (minLength / len);
			else if (len > maxLength)
				return this * (maxLength / len);
			else
				return this;
		}

		public override string ToString()
		{
			return "(" + X + "|" + Y + ")";
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			return "(" + X.ToString(format, formatProvider) + "|" + Y.ToString(format, formatProvider) + ")";
		}

		public static Vector2f Max(Vector2f v1, Vector2f v2)
		{
			return new Vector2f(Math.Max(v1.X, v2.X), Math.Max(v1.Y, v2.Y));
		}

		public static Vector2f Min(Vector2f v1, Vector2f v2)
		{
			return new Vector2f(Math.Min(v1.X, v2.X), Math.Min(v1.Y, v2.Y));
		}

		public static explicit operator Vector2i(Vector2f v)
		{
			return new Vector2i((int)v.X, (int)v.Y);
		}

		public Vector2f Round()
		{
			return new Vector2f((float)Math.Round(X), (float)Math.Round(Y));
		}

		public Vector2i RoundToInt()
		{
			return (Vector2i)this.Round();
		}

		public bool Equals(Vector2f other)
		{
			return X.Equals(other.X) && Y.Equals(other.Y);
		}

		public override bool Equals(object obj)
		{
			if (!(obj.GetType() == typeof(Vector2f)))
				return false;
			return Equals((Vector2f)obj);
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() * 33;
		}
	}
}
