using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util.Mathematics
{
	public struct Plane
	{
		public static Plane PlaneYZ { get { return new Plane(Vector3f.UnitX, 0); } }
		public static Plane PlaneZX { get { return new Plane(Vector3f.UnitY, 0); } }
		public static Plane PlaneXY { get { return new Plane(Vector3f.UnitZ, 0); } }
		public static Plane Null { get { return new Plane(); } }

		public Vector3f Normal { get; private set; }//Normalized unless NullPlane
		public float Offset { get; private set; }

		public double Dist(Vector3f point)
		{
			return Math.Abs(SignedDist(point));
		}

		public double SignedDist(Vector3f point)
		{
			return Vector3f.Dot(Normal, point) + Offset;
		}

		public float Intersect(RayF ray)
		{
			return (-Offset - Vector3f.Dot(ray.Start, Normal)) / (Vector3f.Dot(ray.Direction, Normal));
		}

		public Plane(Vector3f normal, float offset)
			: this()
		{
			Normal = normal.Normalized;
			Offset = offset;
		}

		public Plane(Vector3f normal, Vector3f point)
			: this()
		{
			Normal = normal;
			Offset = -Vector3f.Dot(point, normal);
		}
	}
}
