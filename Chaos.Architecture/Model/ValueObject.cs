using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Architecture.Model
{
	public class ValueObject : BaseObject
	{
		public static bool operator ==(ValueObject left, ValueObject right)
		{
			if ((object)left == (object)right)
				return true;
			if (((object)left == null) ^ ((object)right == null))
				return false;
			return left.Equals(right);
		}

		public static bool operator !=(ValueObject left, ValueObject right)
		{
			return !(left == right);
		}
	}
}
