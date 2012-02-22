using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Architecture.Model
{
	public abstract class Entity
	{
		internal abstract object getIdAsObject();

		public object Id { get { return getIdAsObject(); } }
	}

	public abstract class Entity<TId> : Entity
	{
		internal sealed override object getIdAsObject()
		{
			return Id;
		}

		public virtual new TId Id { get; protected set; }
	}
}
