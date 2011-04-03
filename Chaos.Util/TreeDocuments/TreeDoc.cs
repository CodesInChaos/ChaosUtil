using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.ObjectModel;
using Chaos.Util;
using System.Globalization;

namespace Chaos.Util.TreeDocuments
{
	public class TreeDoc
	{
		#region Core
		private static ReadOnlyCollection<TreeDoc> EmptyReadonlyCollection = new ReadOnlyCollection<TreeDoc>(new TreeDoc[0]);
		private string mValue;
		private List<TreeDoc> mChildren;
		private string mName;

		public string Name
		{
			get { return mName; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("Name");
				mName = value;
			}
		}
		public TreeDoc Parent { get; private set; }
		public int Index { get; private set; }
		public ReadOnlyCollection<TreeDoc> Children { get; private set; }

		public string Value
		{
			get { return mValue; }
			set
			{
				if (mChildren != null)
					throw new InvalidOperationException("Cannot set value on TreeDoc with children. Remove children first.");
				mValue = value;
			}
		}

		public bool HasChildren { get { return mChildren != null; } }

		private void InternalRequireList()
		{
			if (mChildren == null)
			{
				if (Value != null)
					throw new InvalidOperationException("Cannot add children while Value is set. Set Value to null before adding children.");
				mChildren = new List<TreeDoc>();
				Children = new ReadOnlyCollection<TreeDoc>(mChildren);
			}
		}

		private void InternalDestroyList()
		{
			mChildren = null;
			Children = EmptyReadonlyCollection;
		}

		private void InternalDestroyListIfEmpty()
		{
			if (mChildren.Count == 0)
				InternalDestroyList();
		}

		public TreeDoc Next { get { throw new NotImplementedException(); } }
		public TreeDoc Previous { get { throw new NotImplementedException(); } }

		public bool IsAfter(TreeDoc other)
		{
			throw new NotImplementedException();
		}

		public bool IsBefore(TreeDoc other)
		{
			throw new NotImplementedException();
		}

		private TreeDoc GetChildWithIndex(int i)
		{
			if (((uint)i) < mChildren.Count)
				return mChildren[i];
			else
				return null;
		}

		public override string ToString()
		{
			return SaveAsElement();
		}

		public static string ObjectToString(object item)
		{
			return item.ToString();
		}

		public static TreeDoc ObjectToTreeDoc(object item)
		{
			if (item == null)
				return TreeDoc.CreateNull();
			if (item is TreeDoc)
				return (TreeDoc)item;
			if (item is String)
				return TreeDoc.CreateLeaf((String)item);
			Func<TreeDoc> converter = (Func<TreeDoc>)Delegate.CreateDelegate(typeof(Func<TreeDoc>), item, "ToTreeDoc", false, false);
			if (converter != null)
				return converter();
			return TreeDoc.CreateLeaf(ObjectToString(item));
		}
		#endregion

		#region Modification
		public void Insert(int index, TreeDoc item)
		{
			int count;
			if (mChildren != null)
				count = mChildren.Count;
			else
				count = 0;
			if ((uint)index > count)
				throw new ArgumentOutOfRangeException();
			if (item.Parent != null)
				throw new InvalidOperationException("item has a parent already");
			InternalRequireList();
			mChildren.Insert(index, item);
			item.Parent = this;
		}

		public void Insert(int index, object item)
		{
			Insert(index, ObjectToTreeDoc(item));
		}

		public void RemoveAt(int index, TreeDoc item)
		{
			mChildren.RemoveAt(index);
			InternalDestroyListIfEmpty();
		}

		public void RemoveAll()
		{
			mChildren.Clear();
		}

		public void RemoveWhere(Predicate<TreeDoc> doc)
		{
		}

		public void Add(object content)
		{
			if (HasChildren)
				Insert(mChildren.Count, content);
			else
				Insert(0, content);
		}
		public void Add(params object[] content)
		{
			foreach (object o in content)
				Add(o);
		}
		public void AddRange(IEnumerable content)
		{
			foreach (object o in content)
				Add(o);
		}
		public void AddAfterSelf(object o)
		{
			throw new NotImplementedException();
		}

		public void AddAfterSelf(params object[] content)
		{
			throw new NotImplementedException();
		}

		public void AddBeforeSelf(object o)
		{
			throw new NotImplementedException();
		}

		public void AddBeforeSelf(params object[] content)
		{
			throw new NotImplementedException();
		}

		public void Remove()
		{
			throw new NotImplementedException();
		}

		public void ReplaceWith(object replacement)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Construction
		public static TreeDoc CreateList(string name, params object[] children)
		{
			return CreateListRange(name, children);
		}

		public static TreeDoc CreateListRange(string name, IEnumerable children)
		{
			TreeDoc doc = new TreeDoc(name);
			doc.AddRange(children);
			return doc;
		}

		public static TreeDoc CreateListRange(IEnumerable children)
		{
			TreeDoc doc = new TreeDoc("");
			doc.AddRange(children);
			return doc;
		}

		public static TreeDoc CreateLeaf(string name, string value)
		{
			TreeDoc doc = new TreeDoc(name);
			doc.Value = value;
			return doc;
		}
		public static TreeDoc CreateLeaf(string value)
		{
			return CreateLeaf("", value);
		}

		public static TreeDoc CreateNull()
		{
			return CreateNull("");
		}

		public static TreeDoc CreateNull(string name)
		{
			return CreateLeaf(name, null);
		}

		private TreeDoc(string name)
		{
			Name = name;
			Children = EmptyReadonlyCollection;
		}
		#endregion

		#region Linq
		public IEnumerable<TreeDoc> Ancestors()
		{
			return FindAncestorsAndSelf(Parent);
		}
		public IEnumerable<TreeDoc> Ancestors(string name)
		{
			return Ancestors().WhereName(name);
		}
		public IEnumerable<TreeDoc> AncestorsAndSelf()
		{
			return FindAncestorsAndSelf(this);
		}
		public IEnumerable<TreeDoc> AncestorsAndSelf(string name)
		{
			return AncestorsAndSelf().WhereName(name);
		}

		public IEnumerable<TreeDoc> Descendants()
		{
			return this.Elements().SelectMany(child => child.DescendantsAndSelf());
		}
		public IEnumerable<TreeDoc> Descendants(string name)
		{
			return Descendants().WhereName(name);
		}
		public IEnumerable<TreeDoc> DescendantsAndSelf()
		{
			yield return this;
			foreach (TreeDoc doc in Descendants())
				yield return doc;
		}
		public IEnumerable<TreeDoc> DescendantsAndSelf(string name)
		{
			return DescendantsAndSelf().WhereName(name);
		}
		public IEnumerable<TreeDoc> SiblingsAfterSelf()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<TreeDoc> SiblingsAfterSelf(string name)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<TreeDoc> SiblingsBeforeSelf()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<TreeDoc> SiblingsBeforeSelf(string name)
		{
			throw new NotImplementedException();
		}

		private static IEnumerable<TreeDoc> FindAncestorsAndSelf(TreeDoc doc)
		{
			while (doc != null)
			{
				yield return doc;
				doc = doc.Parent;
			}
		}
		#endregion

		#region Serialization

		public bool ForceExpand { get; set; }

		public static TreeDoc Load(string fileName)
		{
			using (TextReader textReader = new StreamReader(fileName, Encoding.UTF8))
			{
				return Load(textReader);
			}
		}
		public static TreeDoc Load(TextReader textReader)
		{
			return Parse(textReader.ReadToEnd());
		}
		public static TreeDoc Parse(string s)
		{
			IEnumerable<TreeDoc> docs = InternalTreeDocParser.ParseDocument(s);
			return TreeDoc.CreateListRange(docs);
		}

		internal static bool UnquotedChar(char c)
		{
			return
				('A' <= c && c <= 'Z') ||
				('a' <= c && c <= 'z') ||
				('0' <= c && c <= '9') ||
				(c == '+') ||
				(c == '-') ||
				(c == '.') ||
				(c == '_') ||
				(c == '*');
		}

		private static string QuotedString(string s)
		{
			s = s
				.Replace("\\", @"\\")
				.Replace("\r", @"\r")
				.Replace("\n", @"\n")
				.Replace("\t", @"\t")
				.Replace("\"", "\\\"")
				.Replace("\0", @"\0");
			return '"' + s + '"';
		}

		private static string AutoQuoteString(string s)
		{
			if (s == "")
				return "\"\"";
			if (s.All(UnquotedChar))
				return s;
			else
				return QuotedString(s);
		}

		private void InternalSave(TextWriter textWriter, HashSet<TreeDoc> expandedNodes, int indentLevel, bool asList)
		{
			bool expand = expandedNodes.Contains(this);
			if (!asList && Name != "")
				textWriter.Write(AutoQuoteString(Name));
			if (Value != null)
			{
				if (Name != "")
					textWriter.Write(':');
				textWriter.Write(AutoQuoteString(Value));
			}
			else
			{
				if (!asList)
					textWriter.Write('(');
				bool first = true;
				foreach (TreeDoc child in Children)
				{
					if (!first)
					{
						textWriter.Write(',');
					}
					if (expand && (!(asList && first)))
					{
						textWriter.WriteLine();
						textWriter.Write(Enumerable.Repeat("\t", indentLevel + 1).ConcatToString());
					}
					first = false;
					child.InternalSave(textWriter, expandedNodes, indentLevel + 1, false);
				}
				if (!asList)
				{
					if (expand)
					{
						textWriter.WriteLine();
						textWriter.Write(Enumerable.Repeat("\t", indentLevel).ConcatToString());
					}
					textWriter.Write(")");
				}
			}
		}
		private void InternalSave(TextWriter textWriter, bool asList)
		{
			HashSet<TreeDoc> expandedNodes = new HashSet<TreeDoc>();
			IEnumerable<TreeDoc> expandedNodesEnumerable =
				DescendantsAndSelf()
				.Where(td => td.ForceExpand)
				.SelectMany(td => td.AncestorsAndSelf());
			foreach (TreeDoc td in expandedNodesEnumerable)
				expandedNodes.Add(td);
			int identLevel = asList ? -1 : 0;
			InternalSave(textWriter, expandedNodes, identLevel, asList);
		}

		public void SaveAsElement(TextWriter textWriter)
		{
			InternalSave(textWriter, false);
		}

		public void SaveAsElement(string fileName)
		{
			using (TextWriter textWriter = new StreamWriter(fileName, false, Encoding.UTF8))
			{
				SaveAsElement(textWriter);
			}
		}

		public void SaveAsList(TextWriter textWriter)
		{
			InternalSave(textWriter, true);
		}

		public string SaveAsElement()
		{
			StringWriter writer = new StringWriter();
			SaveAsElement(writer);
			return writer.ToString();
		}

		public void SaveAsList(string fileName)
		{
			using (TextWriter textWriter = new StreamWriter(fileName, false, Encoding.UTF8))
			{
				SaveAsList(textWriter);
			}
		}

		public string SaveAsList()
		{
			StringWriter writer = new StringWriter();
			SaveAsList(writer);
			return writer.ToString();
		}

		#endregion

		#region Conversion
		public static explicit operator string(TreeDoc td)
		{
			if (td == null)
				return null;
			else if (!td.HasChildren)
				return td.Value;
			else
				throw new InvalidCastException();
		}

		public static explicit operator int(TreeDoc td)
		{
			return int.Parse((string)td);
		}

		public static explicit operator long(TreeDoc td)
		{
			return long.Parse((string)td);
		}

		public static explicit operator bool(TreeDoc td)
		{
			string s = (string)td;
			if (s == "1")
				return true;
			if (s == "0")
				return false;
			throw new InvalidCastException();
		}

		public static explicit operator double(TreeDoc td)
		{
			return double.Parse((string)td, CultureInfo.InvariantCulture);
		}

		public static explicit operator float(TreeDoc td)
		{
			return float.Parse((string)td, CultureInfo.InvariantCulture);
		}

		public static explicit operator float?(TreeDoc td)
		{
			if (td != null)
				return (float)td;
			else
				return null;
		}

		#endregion
	}
}