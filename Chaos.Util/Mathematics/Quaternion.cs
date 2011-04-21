using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Chaos.Util.Mathematics
{
	public struct Quaternion
	{
		private Vector3f v;
		private float w;

		public Vector3f V { get { return v; } }
		public float W { get { return w; } }
		public float X { get { return v.X; } }
		public float Y { get { return v.Y; } }
		public float Z { get { return v.Z; } }

		public Quaternion(float w, Vector3f v)
		{
			this.w = w;
			this.v = v;
		}

		public Quaternion(float w, float x, float y, float z)
		{
			this.w = w;
			this.v = new Vector3f(x, y, z);
		}

		public Quaternion(float w)
		{
			this.w = w;
			this.v = Vector3f.Zero;
		}

		public static Quaternion Zero { get { return new Quaternion(); } }
		public static Quaternion Identity { get { return new Quaternion(1, Vector3f.Zero); } }

		public static Quaternion operator *(Quaternion q1, Quaternion q2)
		{
			throw new NotImplementedException();
		}

		public static Quaternion operator *(Quaternion q, float w)
		{
			return new Quaternion(q.w * w, q.v * w);
		}

		public static Quaternion operator *(float w, Quaternion q)
		{
			return new Quaternion(q.w * w, q.v * w);
		}

		public static Quaternion operator /(Quaternion q, float w)
		{
			return new Quaternion(q.w / w, q.v / w);
		}

		public static Quaternion operator /(Quaternion q1, Quaternion q2)
		{
			return q1 * q2.Inverse;
		}

		public float NormSquared { get { return w * w + v.LengthSquared; } }
		public float Norm { get { return (float)Math.Sqrt(NormSquared); } }
		public Quaternion Normalized { get { return this / Norm; } }
		public Quaternion Conjugate { get { return new Quaternion(w, -v); } }
		public bool IsNormalized { get { return Math.Abs(NormSquared - 1) < 0.0001; } }

		//Requires a normalized quaternion
		public Vector3f RotateVector(Vector3f vec)
		{
			Debug.Assert(IsNormalized);
			throw new NotImplementedException();
		}

		public Quaternion Inverse
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public static Quaternion CreateFromAxisAngle(Vector3f axis, float angle)
		{
			return CreateFromUnitAxisAngle(axis.Normalized, angle);
		}

		public static Quaternion CreateFromUnitAxisAngle(Vector3f axisUnitVector, float angle)
		{
			//Debug.Assert(axisUnitVector.IsUnit);
			float angle2 = 0.5f * angle;
			return new Quaternion((float)Math.Cos(angle2), axisUnitVector * (float)Math.Sin(angle2));
		}
	}
}
