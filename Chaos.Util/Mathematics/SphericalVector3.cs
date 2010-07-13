using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util
{
	public struct SphericalVector3f
	{
		public float Theta { get; private set; }
		public float Phy { get; private set; }
		public float Radius { get; private set; }

		public SphericalVector3f(float theta,float phy,float radius)
			:this()
		{
			Theta = theta;
			Phy = phy;
			Radius = radius;
		}

		public Vector3f ToVector3()
		{
			Vector3f result;
			result.Z = (float)(Radius*Math.Cos(Theta));
			result.X = (float)((Radius*Math.Sin(Theta)) * Math.Cos(Phy));
			result.Y = (float)((Radius*Math.Sin(Theta)) * Math.Sin(Phy));
			return result;
		}
	}
}
