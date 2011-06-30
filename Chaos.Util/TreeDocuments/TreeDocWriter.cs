using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Chaos.Util.TreeDocuments
{
	public class TreeDocWriter : IDisposable
	{
		private readonly string indent;
		private readonly TextWriter textWriter;
		private int depth;
		private int expandDepth;

		public string Indent { get { return indent; } }
		public TextWriter TextWriter { get { return textWriter; } }
		public int Depth { get { return depth; } }
		private bool Expanded { get { return depth == expandDepth; } }

		private void WriteKey(string key)
		{
		}

		public void WriteString(string key, string value)
		{
		}

		public void WriteNil(string value)
		{
		}

		public void BeginTree(string key, bool expand)
		{
			//if (!Expanded && expanded)
			//	throw new ArgumentException("Can't expand a node inside a not expanded parent node", "expand");
			
		}

		public void EndTree()
		{
		}

		public void Close()
		{
		}

		public TreeDocWriter(TextWriter textWriter, string indent)
		{
			this.textWriter = textWriter;
			this.indent = indent;
		}

		public TreeDocWriter(TextWriter textWriter)
			: this(textWriter, "\t")
		{

		}

		void IDisposable.Dispose()
		{
			Close();
		}
	}
}
