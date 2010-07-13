using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Util.TreeDocuments
{
	public static class TreeDocExtensions
	{
		public static IEnumerable<TreeDoc> WhereName(this IEnumerable<TreeDoc> docs, string name)
		{
			return docs.Where(doc => doc.Name == name);
		}

		//On TreeDoc, supports this==null
		public static TreeDoc Element(this TreeDoc doc, string name)
		{
			return doc.Elements(name).FirstOrDefault();
		}

		public static TreeDoc Element(this TreeDoc doc, string name,int i)
		{
			return doc.Elements(name).Skip(i).FirstOrDefault();
		}

		public static IEnumerable<TreeDoc> Elements(this TreeDoc doc)
		{
			if (doc == null)
				return Enumerable.Empty<TreeDoc>();
			else
				return doc.Children;
		}

		public static IEnumerable<TreeDoc> Elements(this TreeDoc doc, string name)
		{
			return doc.Elements().WhereName(name);
		}

		//On IEnumerable<TreeDoc> supports null
		public static TreeDoc Element(this IEnumerable<TreeDoc> docs, string name)
		{
			return docs.Elements(name).FirstOrDefault();
		}

		public static IEnumerable<TreeDoc> Elements(this IEnumerable<TreeDoc> docs)
		{
			return docs.SelectMany(doc=>doc.Elements());
		}

		public static IEnumerable<TreeDoc> Elements(this IEnumerable<TreeDoc> docs, string name)
		{
			return docs.Elements().WhereName(name);
		}

	}
}
