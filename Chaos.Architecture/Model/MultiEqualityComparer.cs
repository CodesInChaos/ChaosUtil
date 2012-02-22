using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Chaos.Util.Model
{
	/*public abstract class MultiEqualityComparer
	{
		internal class ComparisonProperty<TObject, TProperty>
		{
			private readonly Expression<Func<TObject, TProperty>> _property;
			private readonly IEqualityComparer<TProperty> _comparer;
			private EqualityComparer<TProperty> equalityComparer;

			public ComparisonProperty(Expression<Func<TObject, TProperty>> property, EqualityComparer<TProperty> equalityComparer)
			{
				_property = property;
				_comparer = equalityComparer;
			}

			public Expression<Func<TObject, TProperty>> Property { get { return _property; } }
			public IEqualityComparer<TProperty> Comparer { get { return _comparer; } }
		}

		public abstract Type Type { get; }

		internal abstract void Add<TObject, TProperty>(ComparisonProperty<TObject, TProperty> comparisonProperty);

		public void Add(PropertyInfo prop)
		{
			Type objectType = prop.DeclaringType;
			Type propertyType = prop.PropertyType;
		}

		public void Add<TObject, TProperty>(Expression<Func<TObject, TProperty>> property)
		{
			Add(property, EqualityComparer<TProperty>.Default);
		}

		void Add<TObject, TProperty>(Expression<Func<TObject, TProperty>> property, EqualityComparer<TProperty> equalityComparer)
		{
			Add(new ComparisonProperty<TObject, TProperty>(property, equalityComparer));
		}
	}

	public class MultiComparer<T> : MultiEqualityComparer, IEqualityComparer<T>
	{
		public override Type Type
		{
			get { return typeof(T); }
		}

		public bool Equals(T x, T y)
		{
			throw new NotImplementedException();
		}

		public int GetHashCode(T obj)
		{
			throw new NotImplementedException();
		}
	}*/
}
