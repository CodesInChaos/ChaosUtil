using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util
{
	public struct Circle
	{
		public Vector2f Center { get; private set; }
		public float Radius { get; private set; }
		public float RadiusSquared { get { return Radius * Radius; } }

		public RectangleF BoundingBox { get { return new RectangleF(Center.X - Radius, Center.Y - Radius, Center.X + Radius, Center.Y + Radius); } }
		public bool Contains(Vector2f v)
		{
			return (v - Center).LengthSquared <= RadiusSquared;
		}

		public Circle(Vector2f center, float radius)
			: this()
		{
			Center = center;
			Radius = radius;
			if (!(Radius >= 0))
				throw new ArgumentException("Radius must be >=0");
		}
	}
}
