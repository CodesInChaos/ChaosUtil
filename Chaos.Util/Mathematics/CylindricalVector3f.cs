using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util.Mathematics
{
	public struct CylindricalVector3f
	{
		public readonly float Z;
		public readonly float Rho;
		public readonly float Phi;
		public float SphericalRadius { get { return Rho * Rho + Z * Z; } }
		public float Theta { get { return (float)Math.Atan(Z / Rho); } }

		public SphericalVector3f ToSpherical()
		{
			return new SphericalVector3f(Theta, Phi, SphericalRadius);
		}
	}
}
