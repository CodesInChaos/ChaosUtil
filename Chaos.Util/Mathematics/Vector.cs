using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util.Mathematics
{
	public static class Vector
	{
		public static float Dot(Vector2f vL, Vector2f vR)
		{
			return Vector2f.Dot(vL, vR);
		}

		public static float Dot(Vector3f vL, Vector3f vR)
		{
			return Vector3f.Dot(vL, vR);
		}

		/*public float Dot(Vector4f vL, Vector4f vR)
		{
			return Vector4f.Dot(vL, vR);
		}*/

		public static Vector3f Cross(Vector3f vL, Vector3f vR)
		{
			return Vector3f.Cross(vL, vR);
		}
	}
}
