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

		public static FieldInfo GetBackingField(PropertyInfo property)
		{
			//Fixme: Add caching
			return GetBackingFieldInternal(property);
		}

		//Supports getters of the form `return field;`
		private static FieldInfo GetBackingFieldInternal(PropertyInfo property)
		{
			if (property == null)
				throw new ArgumentNullException("property");
			MethodInfo getter = property.GetGetMethod(true);
			if (getter == null)
				return null;
			byte[] il = getter.GetMethodBody().GetILAsByteArray();
			if (il.Length != 7
			   || il[0] != 0x02//ldarg.0
			   || il[1] != 0x7b//ldfld <field>
			   || il[6] != 0x2a//ret
			   )
				return null;
			int metadataToken = il[2] | il[3] << 8 | il[4] << 16 | il[5] << 24;
			Type type = property.ReflectedType;
			do
			{
				foreach (FieldInfo fi in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (fi.MetadataToken == metadataToken)
						return fi;
				}
				type = type.BaseType;
			} while (type != null);
			throw new Exception("Field not found");
		}
	}
}
