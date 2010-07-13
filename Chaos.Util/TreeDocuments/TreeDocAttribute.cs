using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util.TreeDocuments
{
	public class TreeDocAttribute : Attribute
	{
		public string Name { get; private set; }
		public int Index { get; private set; }
		public TreeDocAttribute(string name, int index)
		{
			Name = name;
			Index = index;
		}

		public TreeDocAttribute(string name)
			: this(name, 0)
		{
		}

		public TreeDocAttribute(int index)
			: this("", index)
		{
		}
	}
}
