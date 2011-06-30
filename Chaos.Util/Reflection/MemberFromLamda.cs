using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.Diagnostics.Contracts;

namespace Chaos.Util.Reflection
{
	public static class MemberFromLamda
	{
		public static MemberInfo MemberInfo(Expression exp)
		{
			Contract.Requires<ArgumentNullException>(exp != null);
			var lambda = exp as LambdaExpression;
			if (lambda == null)
				throw new ArgumentException("exp is no LamdaExpression but a " + exp.GetType().FullName);
			var body = lambda.Body as MemberExpression;
			if (body == null)
				throw new ArgumentException("exp.Body is no Member expression but a " + lambda.Body.GetType());
			return body.Member;
		}

		public static PropertyInfo PropertyInfo(Expression exp)
		{
			Contract.Requires<ArgumentNullException>(exp != null);
			return (PropertyInfo)MemberInfo(exp);
		}

		public static FieldInfo FieldInfo(Expression exp)
		{
			Contract.Requires<ArgumentNullException>(exp != null);
			return (FieldInfo)MemberInfo(exp);
		}

		public static string Name(Expression exp)
		{
			Contract.Requires<ArgumentNullException>(exp != null);
			return MemberInfo(exp).Name;
		}
	}
}
