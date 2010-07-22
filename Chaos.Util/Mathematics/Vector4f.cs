using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util.Mathematics
{
	public struct Vector4f
	{
		public float X;
		public float Y;
		public float Z;
		public float W;

		public float this[int i]
		{
			get
			{
				switch (i)
				{
					case 0: { return X; }
					case 1: { return Y; }
					case 2: { return Z; }
					case 3: { return W; }
					default: { throw new ArgumentException(); }
				}
			}
			set
			{
				switch (i)
				{
					case 0: { X = value; break; }
					case 1: { Y = value; break; }
					case 2: { Z = value; break; }
					case 3: { W = value; break; }
					default: { throw new ArgumentException(); }
				}
			}
		}


		public Vector4f(float x, float y, float z, float w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}

		public Vector4f(Vector2f v, float z, float w)
		{
			X = v.X;
			Y = v.Y;
			Z = z;
			W = w;
		}

		public Vector4f(Vector3f v, float w)
		{
			X = v.X;
			Y = v.Y;
			Z = v.Z;
			W = w;
		}

		public static Vector4f Zero { get { return new Vector4f(); } }
		public static Vector4f UnitX { get { return new Vector4f(1, 0, 0, 0); } }
		public static Vector4f UnitY { get { return new Vector4f(0, 1, 0, 0); } }
		public static Vector4f UnitZ { get { return new Vector4f(0, 0, 1, 0); } }
		public static Vector4f UnitW { get { return new Vector4f(0, 0, 0, 1); } }

		public Vector2f XY { get { return new Vector2f(X, Y); } }
		public Vector2f WZ { get { return new Vector2f(W, Z); } }
		public Vector3f XYZ { get { return new Vector3f(X, Y, Z); } }

		public override string ToString()
		{
			return "(" + X + "|" + Y + "|" + Z + "|" + W + ")";
		}

		public float Length { get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W); } }
		public float LengthSquared { get { return X * X + Y * Y + Z * Z + W * W; } }

		/*public Vector4f Clamp(float maxLength)
		{
			float len = Length;
			if (len > maxLength)
				return this * (maxLength / len);
			else
				return this;
		}

		public Vector4f Clamp(float minLength, float maxLength)
		{
			float len = Length;
			if (len < minLength)
				return this * (minLength / len);
			else if (len > maxLength)
				return this * (maxLength / len);
			else
				return this;
		}*/

		public static float operator *(Vector4f vL, Vector4f vR)
		{
			return vL.X * vR.X + vL.Y * vR.Y + vL.Z * vR.Z + vL.W * vR.W;
		}

		public static Vector4f operator +(Vector4f vL, Vector4f vR)
		{
			return new Vector4f(vL.X + vR.X, vL.Y + vR.Y, vL.Z + vR.Z, vL.W + vR.W);
		}

		public static Vector4f operator -(Vector4f vL, Vector4f vR)
		{
			return new Vector4f(vL.X - vR.X, vL.Y - vR.Y, vL.Z - vR.Z, vL.W - vR.W);
		}

		public static Vector4f operator -(Vector4f v)
		{
			return new Vector4f(-v.X, -v.Y, -v.Z, -v.W);
		}
	}
}
