using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util.Mathematics
{
	public struct Vector3f
	{
		public float X;
		public float Y;
		public float Z;

		public Vector3f(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public Vector3f(Vector2f v, float z)
		{
			X = v.X;
			Y = v.Y;
			Z = z;
		}

		public static Vector3f Zero { get { return new Vector3f(); } }
		public static Vector3f UnitX { get { return new Vector3f(1, 0, 0); } }
		public static Vector3f UnitY { get { return new Vector3f(0, 1, 0); } }
		public static Vector3f UnitZ { get { return new Vector3f(0, 0, 1); } }
		public static Vector3f One { get { return new Vector3f(1, 1, 1); } }

		public float LengthSquared { get { return X * X + Y * Y + Z * Z; } }
		public float Length { get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); } }

		public Vector2f XY { get { return new Vector2f(X, Y); } }

		public static Vector3f operator *(Vector3f v, float s)
		{
			return new Vector3f(v.X * s, v.Y * s, v.Z * s);
		}

		public static Vector3f operator *(float s, Vector3f v)
		{
			return new Vector3f(v.X * s, v.Y * s, v.Z * s);
		}

		public static Vector3f operator /(Vector3f v, float s)
		{
			return v * (1 / s);
		}

		public static Vector3f operator -(Vector3f v)
		{
			return new Vector3f(-v.X, -v.Y, -v.Z);
		}

		public static Vector3f operator +(Vector3f v1, Vector3f v2)
		{
			return new Vector3f(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
		}

		public static Vector3f operator -(Vector3f v1, Vector3f v2)
		{
			return new Vector3f(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
		}

		public static Vector3f operator *(Vector3f v1, Vector3f v2)
		{
			return new Vector3f(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
		}

		public static float Dot(Vector3f v1, Vector3f v2)
		{
			return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
		}

		public static Vector3f Cross(Vector3f v1, Vector3f v2)
		{
			throw new NotImplementedException();
			/*return new Vector3f(
				v1.Y * v2.Z - v2.Y * v1.Z,
				v1.Z * v2.X - v2.X * v1.Z,
				v1.X * v2.Z - v2.Y * v1.X
				);*/
		}

		public static Vector3f ComponentMultiply(Vector3f v1, Vector3f v2)
		{
			return new Vector3f(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
		}

		public static Vector3f ComponentDivide(Vector3f v1, Vector3f v2)
		{
			return new Vector3f(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z);
		}

		public Vector3f Normalized { get { return this / Length; } }

		public static float Distance(Vector3f v1, Vector3f v2)
		{
			return (v1 - v2).Length;
		}

		public static float DistanceSquared(Vector3f v1, Vector3f v2)
		{
			return (v1 - v2).LengthSquared;
		}

		public Vector3f Clamp(float maxLength)
		{
			float len = Length;
			if (len > maxLength)
				return this * (maxLength / len);
			else
				return this;
		}

		public Vector3f Clamp(float minLength, float maxLength)
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
			return "(" + X + "|" + Y + "|" + Z + ")";
		}
	}
}
