using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Chaos.Util.Mathematics
{
	public struct CircleF
	{
		private readonly Vector2f center;
		private readonly float radius;

		public Vector2f Center
		{
			get
			{
				return center;
			}
		}

		public float Radius
		{
			get
			{
				Contract.Ensures(Contract.Result<float>() >= 0);
				return radius;
			}
		}

		public float RadiusSquared
		{
			get
			{
				Contract.Ensures(Contract.Result<float>() >= 0);
				return Radius * Radius;
			}
		}

		public RectangleF BoundingBox { get { return RectangleF.FromLTRB(Center.X - Radius, Center.Y - Radius, Center.X + Radius, Center.Y + Radius); } }
		public bool Contains(Vector2f v)
		{
			return (v - Center).LengthSquared <= RadiusSquared;
		}

		public CircleF(Vector2f center, float radius)
		{
			Contract.Requires<ArgumentException>(radius >= 0);
			this.center = center;
			this.radius = radius;
		}
	}
}
