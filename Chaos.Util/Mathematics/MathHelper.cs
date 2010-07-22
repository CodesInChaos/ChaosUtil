using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util.Mathematics
{
	public static class MathHelper
	{
		public static double Clamp(this double value, double min, double max)
		{
			if (value < min)
				return min;
			else if (value > max)
				return max;
			else
				return value;
		}

		public static float Clamp(this float value, float min, float max)
		{
			if (value < min)
				return min;
			else if (value > max)
				return max;
			else
				return value;
		}

		public static double DegToRad(double deg)
		{
			return deg * (Math.PI / 180.0);
		}

		public static double RadToDeg(double rad)
		{
			return rad * (180.0 / Math.PI);
		}

		public static double Lerp(double value1, double value2, double amount)
		{
			return (1 - amount) * value1 + amount * value2;
		}

		public static double ClampedLerp(double value1, double value2, double amount)
		{
			amount = amount.Clamp(0, 1);
			return (1 - amount) * value1 + amount * value2;
		}

		public static double WrapAngle(double angle)
		{
			return Math.IEEERemainder(angle, 2 * Math.PI);
		}

		public static int Square(int x)
		{
			return x * x;
		}

		public static float Square(float x)
		{
			return x * x;
		}

		public static double Square(double x)
		{
			return x * x;
		}
	}
}
