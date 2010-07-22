using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util.Mathematics
{
	//Immutable
	public struct RayF
	{
		public Vector3f AtParam(float param)
		{
			return Start + param * Direction;
		}

		public Vector3f Start { get; private set; }
		public Vector3f Direction { get; private set; }//Normalized

		public RayF(Vector3f start, Vector3f direction)
			:this()
		{
			Direction = direction.Normalized;
			Start = start;
		}
	}
}
