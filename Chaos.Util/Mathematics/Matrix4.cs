using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util
{
	public struct Matrix4f
	{
		private Vector4f row0;
		private Vector4f row1;
		private Vector4f row2;
		private Vector4f row3;

		public Vector4f Row0 { get { return row0; } }
		public Vector4f Row1 { get { return row1; } }
		public Vector4f Row2 { get { return row2; } }
		public Vector4f Row3 { get { return row3; } }

		public Vector4f Col0 { get { return new Vector4f(row0.X, row1.X, row2.X, row3.X); } }
		public Vector4f Col1 { get { return new Vector4f(row0.Y, row1.Y, row2.Y, row3.Y); } }
		public Vector4f Col2 { get { return new Vector4f(row0.Z, row1.Z, row2.Z, row3.Z); } }
		public Vector4f Col3 { get { return new Vector4f(row0.W, row1.W, row2.W, row3.W); } }

		public static Matrix4f Zero { get { return new Matrix4f(); } }
		public static Matrix4f Identity { get { return new Matrix4f(Vector4f.UnitX, Vector4f.UnitY, Vector4f.UnitZ, Vector4f.UnitW); } }

		public Matrix4f(Vector4f row0, Vector4f row1, Vector4f row2, Vector4f row3)
		{
			this.row0 = row0;
			this.row1 = row1;
			this.row2 = row2;
			this.row3 = row3;
		}

		public Vector4f Row(int i)
		{
			return this[i];
		}

		public Vector4f Col(int i)
		{
			switch (i)
			{
				case 0: { return Col0; }
				case 1: { return Col1; }
				case 2: { return Col2; }
				case 3: { return Col3; }
				default: { throw new ArgumentException(); }
			}
		}

		public Vector4f this[int row]
		{
			get
			{
				switch (row)
				{
					case 0: { return row0; }
					case 1: { return row1; }
					case 2: { return row2; }
					case 3: { return row3; }
					default: { throw new ArgumentException(); }
				}
			}
		}

		public float this[int row, int col]
		{
			get
			{
				switch (row)
				{
					case 0: { return row0[col]; }
					case 1: { return row1[col]; }
					case 2: { return row2[col]; }
					case 3: { return row3[col]; }
					default: { throw new ArgumentException(); }
				}
			}
			set
			{
				switch (row)
				{
					case 0: { row0[col] = value; break; }
					case 1: { row1[col] = value; break; }
					case 2: { row2[col] = value; break; }
					case 3: { row3[col] = value; break; }
					default: { throw new ArgumentException(); }
				}
			}
		}

		public static Vector4f operator *(Matrix4f mL, Vector4f vR)
		{
			return new Vector4f(mL.row0 * vR, mL.row1 * vR, mL.row2 * vR, mL.row3 * vR);
		}

		public static Matrix4f operator *(Matrix4f mL, Matrix4f mR)
		{
			Matrix4f result = Matrix4f.Zero;
			for (int row = 0; row < 4; row++)
				for (int col = 0; col < 4; col++)
					result[row, col] = mL.Row(row) * mL.Col(col);
			return result;
		}

		public Matrix4f Transposed()
		{
			return new Matrix4f(Col0, Col1, Col2, Col3);
		}

		public static Matrix4f CreateTranslation(Vector3f v)
		{
			return new Matrix4f(Vector4f.UnitX, Vector4f.UnitY, Vector4f.UnitZ, new Vector4f(v, 0));
		}

		/// <summary>
		/// Requires Col(3)==UnitW
		/// </summary>
		public Matrix4f AppendTranslation(Vector3f v)
		{
			Matrix4f result = this;
			result.row3 += new Vector4f(v, 0);
			return result;
		}

		public Matrix4f Inverse()
		{
			Matrix4f result;
			float invDet = 1 / Determinant();
			result.row0.X = (row1.Z * row2.W * row3.Y - row1.W * row2.Z * row3.Y + row1.W * row2.Y * row3.Z - row1.Y * row2.W * row3.Z - row1.Z * row2.Y * row3.W + row1.Y * row2.Z * row3.W) * invDet;
			result.row0.Y = (row0.W * row2.Z * row3.Y - row0.Z * row2.W * row3.Y - row0.W * row2.Y * row3.Z + row0.Y * row2.W * row3.Z + row0.Z * row2.Y * row3.W - row0.Y * row2.Z * row3.W) * invDet;
			result.row0.Z = (row0.Z * row1.W * row3.Y - row0.W * row1.Z * row3.Y + row0.W * row1.Y * row3.Z - row0.Y * row1.W * row3.Z - row0.Z * row1.Y * row3.W + row0.Y * row1.Z * row3.W) * invDet;
			result.row0.W = (row0.W * row1.Z * row2.Y - row0.Z * row1.W * row2.Y - row0.W * row1.Y * row2.Z + row0.Y * row1.W * row2.Z + row0.Z * row1.Y * row2.W - row0.Y * row1.Z * row2.W) * invDet;
			result.row1.X = (row1.W * row2.Z * row3.X - row1.Z * row2.W * row3.X - row1.W * row2.X * row3.Z + row1.X * row2.W * row3.Z + row1.Z * row2.X * row3.W - row1.X * row2.Z * row3.W) * invDet;
			result.row1.Y = (row0.Z * row2.W * row3.X - row0.W * row2.Z * row3.X + row0.W * row2.X * row3.Z - row0.X * row2.W * row3.Z - row0.Z * row2.X * row3.W + row0.X * row2.Z * row3.W) * invDet;
			result.row1.Z = (row0.W * row1.Z * row3.X - row0.Z * row1.W * row3.X - row0.W * row1.X * row3.Z + row0.X * row1.W * row3.Z + row0.Z * row1.X * row3.W - row0.X * row1.Z * row3.W) * invDet;
			result.row1.W = (row0.Z * row1.W * row2.X - row0.W * row1.Z * row2.X + row0.W * row1.X * row2.Z - row0.X * row1.W * row2.Z - row0.Z * row1.X * row2.W + row0.X * row1.Z * row2.W) * invDet;
			result.row2.X = (row1.Y * row2.W * row3.X - row1.W * row2.Y * row3.X + row1.W * row2.X * row3.Y - row1.X * row2.W * row3.Y - row1.Y * row2.X * row3.W + row1.X * row2.Y * row3.W) * invDet;
			result.row2.Y = (row0.W * row2.Y * row3.X - row0.Y * row2.W * row3.X - row0.W * row2.X * row3.Y + row0.X * row2.W * row3.Y + row0.Y * row2.X * row3.W - row0.X * row2.Y * row3.W) * invDet;
			result.row2.Z = (row0.Y * row1.W * row3.X - row0.W * row1.Y * row3.X + row0.W * row1.X * row3.Y - row0.X * row1.W * row3.Y - row0.Y * row1.X * row3.W + row0.X * row1.Y * row3.W) * invDet;
			result.row2.W = (row0.W * row1.Y * row2.X - row0.Y * row1.W * row2.X - row0.W * row1.X * row2.Y + row0.X * row1.W * row2.Y + row0.Y * row1.X * row2.W - row0.X * row1.Y * row2.W) * invDet;
			result.row3.X = (row1.Z * row2.Y * row3.X - row1.Y * row2.Z * row3.X - row1.Z * row2.X * row3.Y + row1.X * row2.Z * row3.Y + row1.Y * row2.X * row3.Z - row1.X * row2.Y * row3.Z) * invDet;
			result.row3.Y = (row0.Y * row2.Z * row3.X - row0.Z * row2.Y * row3.X + row0.Z * row2.X * row3.Y - row0.X * row2.Z * row3.Y - row0.Y * row2.X * row3.Z + row0.X * row2.Y * row3.Z) * invDet;
			result.row3.Z = (row0.Z * row1.Y * row3.X - row0.Y * row1.Z * row3.X - row0.Z * row1.X * row3.Y + row0.X * row1.Z * row3.Y + row0.Y * row1.X * row3.Z - row0.X * row1.Y * row3.Z) * invDet;
			result.row3.W = (row0.Y * row1.Z * row2.X - row0.Z * row1.Y * row2.X + row0.Z * row1.X * row2.Y - row0.X * row1.Z * row2.Y - row0.Y * row1.X * row2.Z + row0.X * row1.Y * row2.Z) * invDet;
			return result;
		}

		public float Determinant()
		{
			return
				row0.W * row1.Z * row2.Y * row3.X - row0.Z * row1.W * row2.Y * row3.X - row0.W * row1.Y * row2.Z * row3.X + row0.Y * row1.W * row2.Z * row3.X +
				row0.Z * row1.Y * row2.W * row3.X - row0.Y * row1.Z * row2.W * row3.X - row0.W * row1.Z * row2.X * row3.Y + row0.Z * row1.W * row2.X * row3.Y +
				row0.W * row1.X * row2.Z * row3.Y - row0.X * row1.W * row2.Z * row3.Y - row0.Z * row1.X * row2.W * row3.Y + row0.X * row1.Z * row2.W * row3.Y +
				row0.W * row1.Y * row2.X * row3.Z - row0.Y * row1.W * row2.X * row3.Z - row0.W * row1.X * row2.Y * row3.Z + row0.X * row1.W * row2.Y * row3.Z +
				row0.Y * row1.X * row2.W * row3.Z - row0.X * row1.Y * row2.W * row3.Z - row0.Z * row1.Y * row2.X * row3.W + row0.Y * row1.Z * row2.X * row3.W +
				row0.Z * row1.X * row2.Y * row3.W - row0.X * row1.Z * row2.Y * row3.W - row0.Y * row1.X * row2.Z * row3.W + row0.X * row1.Y * row2.Z * row3.W;
		}

		public static Matrix4f CreatePerspective(float fovY, float aspect, float near, float far)
		{
			float fy = (float)(1 / Math.Tan(fovY / 2));
			float fx = fy / aspect;
			return CreatePerspective2(fx, fy, near, far);
		}

		public static Matrix4f CreatePerspective2(float fx, float fy, float near, float far)
		{
			Matrix4f result = Matrix4f.Zero;
			result[0, 0] = fx;
			result[1, 1] = fy;
			result[2, 2] = (far + near) / (far - near);
			result[2, 3] = 2 * far * near / (near - far);
			result[3, 2] = -1;
			return result;
		}

		public Vector3f TransformAbsolute(Vector3f v)
		{
			return (this * new Vector4f(v, 1)).XYZ;
		}

		public Vector3f TransformRelative(Vector3f v)
		{
			return (this * new Vector4f(v, 0)).XYZ;
		}

		public static Matrix4f CreateRotation(float angle, Vector3f axis)
		{
			Vector3f u = axis.Normalized;
			float c = (float)Math.Cos(angle);
			float s = (float)Math.Sin(angle);
			Matrix4f result = Matrix4f.Identity;
			result[0, 0] = u.X * u.X + (1 - u.X * u.X) * c;
			result[0, 1] = u.X * u.Y * (1 - c) - u.Z * s;
			result[0, 2] = u.X * u.Z * (1 - c) + u.Y * s;
			result[1, 0] = u.X * u.Y * (1 - c) + u.Z * s;
			result[1, 1] = u.Y * u.Y + (1 - u.Y * u.Y) * c;
			result[1, 2] = u.Y * u.Z * (1 - c) - u.X * s;
			result[2, 0] = u.X * u.Z * (1 - c) - u.Y * s;
			result[2, 1] = u.Y * u.Z * (1 - c) + u.X * s;
			result[2, 2] = u.Z * u.Z + (1 - u.Z * u.Z) * c;
			return result;
		}
	}
}
