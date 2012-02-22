using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Numerics.Visualization
{
	interface IDiagramAxis
	{
		double ValueAtPosition(double position);
		double PositionOfValue(double value);
	}
}
