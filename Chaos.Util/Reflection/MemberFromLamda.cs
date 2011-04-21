using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Chaos.Util.Reflection
{
	public static class MemberFromLamda
	{
		public static MemberInfo MemberInfo(Expression exp)
		{
			throw new NotImplementedException();
		}

		public static PropertyInfo PropertyInfo(Expression exp)
		{
			return (PropertyInfo)MemberInfo(exp);
		}
	}
}
