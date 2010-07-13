using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Chaos.Util.TreeDocuments
{
	internal class InternalTreeDocParser
	{
		readonly string s;

		public InternalTreeDocParser(string s)
		{
			this.s = s;
		}

		public char? GetChar(int i)
		{
			if (i < s.Length)
				return s[i];
			else
				return null;
		}

		private readonly HashSet<char?> whitespaces = new HashSet<char?>() { ' ', '\r', '\n', '\t', (char)0xFEFF };
		public void SkipWhiteSpace(ref int i)
		{
			while (whitespaces.Contains(GetChar(i)))
				i++;
		}

		public string ParseString(ref int i)
		{
			StringBuilder sb = new StringBuilder();
			char? c = GetChar(i);
			if (c == '"')
			{
				if (c == null)
					throw new InvalidDataException("Unexpected end of data");

				bool escaped = false;
				do
				{
					i++;
					c = GetChar(i);
					if (escaped)
					{
						escaped = false;
						switch (c)
						{
							case 'r':
								sb.Append('\r');
								break;
							case 'n':
								sb.Append('\n');
								break;
							case 't':
								sb.Append('\t');
								break;
							case '0':
								sb.Append('\0');
								break;
							case '\\':
							case '"':
								sb.Append((char)c);
								break;
							default:
								throw new InvalidDataException("Unknown escape sequence '\\" + c + "' at " + (i - 1));
						}
					}
					else
					{
						switch (c)
						{
							case '"':
								i++;
								return sb.ToString();
							case '\\':
								escaped = true;
								break;
							default:
								sb.Append((char)c);
								break;
						}
					}
				} while (true);

			}
			else
			{
				if (!TreeDoc.UnquotedChar((char)c))
					throw new InvalidDataException("Unexpected char " + GetChar(i) + " at " + i);
				do
				{
					sb.Append((char)c);
					i++;
					c = GetChar(i);
				} while (c != null && TreeDoc.UnquotedChar((char)c));
				return sb.ToString();
			}
		}

		public TreeDoc ParseEntry(ref int i)
		{
			SkipWhiteSpace(ref i);
			if (GetChar(i) == '(')
			{
				i++;
				TreeDoc result = TreeDoc.CreateListRange(ParseList(ref i));
				if (GetChar(i) != ')')
					throw new InvalidDataException("Unexpected char " + GetChar(i) + " at " + i + " expected )");
				i++;
				return result;
			}
			else
			{
				string text = ParseString(ref i);
				SkipWhiteSpace(ref i);
				switch (GetChar(i))
				{
					case null:
					case ',':
					case ')':
						return TreeDoc.CreateLeaf(text);
					case ':':
						i++;
						string value = ParseString(ref i);
						return TreeDoc.CreateLeaf(text, value);
					case '(':
						i++;
						TreeDoc entry = TreeDoc.CreateListRange(text, ParseList(ref i));
						SkipWhiteSpace(ref i);
						if (GetChar(i) != ')')
							throw new InvalidDataException("Unexpected char " + GetChar(i) + " at " + i + " expected )");
						i++;
						return entry;
					default:
						throw new InvalidDataException("Unexpected char " + GetChar(i) + " at " + i + "expected ,):(");
				}
			}
		}

		public IEnumerable<TreeDoc> ParseList(ref int i)
		{
			List<TreeDoc> result = new List<TreeDoc>();
			SkipWhiteSpace(ref i);
			if (GetChar(i) == ')')
				return result;

			while (true)
			{
				SkipWhiteSpace(ref i);
				TreeDoc td = ParseEntry(ref i);
				result.Add(td);
				SkipWhiteSpace(ref i);
				switch (GetChar(i))
				{
					case null:
					case ')':
						return result;
					case ',':
						i++;
						break;
					default:
						throw new InvalidDataException("Unexpected char " + GetChar(i) + " at " + i);
				}
			}
		}

		public IEnumerable<TreeDoc> ParseDocument()
		{
			int i = 0;
			IEnumerable<TreeDoc> docs = ParseList(ref i);
			SkipWhiteSpace(ref i);
			if (GetChar(i) != null)
				throw new InvalidDataException("Unexpected char " + GetChar(i) + " at " + i);
			return docs;
		}

		public static IEnumerable<TreeDoc> ParseDocument(string s)
		{
			return new InternalTreeDocParser(s).ParseDocument();
		}
	}
}
