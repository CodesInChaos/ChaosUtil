using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Chaos.Util
{
	public static class Conversion
	{
		public static bool TryParseHex(string s, out UInt32 result)
		{
			return UInt32.TryParse(s, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out result);
		}

		public static UInt32 ParseHex(string s)
		{
			return UInt32.Parse(s, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
		}

		public static string ToHex(this UInt32 i)
		{
			return i.ToString("X");
		}

		public static string ToHex(this Int32 i)
		{
			return i.ToString("X");
		}

		public static string ToHex(this UInt32 i, int digits)
		{
			return i.ToString("X" + digits + "");
		}

		public static string ToHex(this Int32 i, int digits)
		{
			return i.ToString("X" + digits + "");
		}

		public static T ConvertDelegate<T>(Delegate d)
		{
			if (!(typeof(T).IsSubclassOf(typeof(Delegate))))
				throw new ArgumentException("T is no Delegate");
			if (d == null)
				throw new ArgumentNullException();
			MulticastDelegate md = d as MulticastDelegate;
			Delegate[] invList = null;
			int invCount = 1;
			if (md != null)
				invList = md.GetInvocationList();
			if (invList != null)
				invCount = invList.Length;
			if (invCount == 1)
			{
				return (T)(object)Delegate.CreateDelegate(typeof(T), d.Target, d.Method);
			}
			else
			{
				for (int i = 0; i < invList.Length; i++)
				{
					invList[i] = (Delegate)(object)ConvertDelegate<T>(invList[i]);
				}
				return (T)(object)MulticastDelegate.Combine(invList);
			}
		}

		private static readonly char[] hexchars0 = new char[256];
		private static readonly char[] hexchars1 = new char[256];
		public static string ToHex(this byte[] buffer)
		{
			StringBuilder sb = new StringBuilder(buffer.Length * 2);
			for (int i = 0; i < buffer.Length; i++)
			{
				sb[2 * i] = hexchars0[buffer[i]];
				sb[2 * i + 1] = hexchars1[buffer[i]];
			}
			return sb.ToString();
		}

		static Conversion()
		{
			for (int i = 0; i < 256; i++)
			{
				string h = i.ToHex(2);
				hexchars0[i] = h[0];
				hexchars1[i] = h[1];
			}
		}

		private static class ParseDelegateStore<T>
		{
			public static ParseDelegate<T> Parse;
			public static TryParseDelegate<T> TryParse;
		}

		private delegate T ParseDelegate<T>(string s);
		private delegate bool TryParseDelegate<T>(string s, out T result);


		public static T Parse<T>(string s)
		{
			ParseDelegate<T> parse = ParseDelegateStore<T>.Parse;
			if (parse == null)
			{
				parse = (ParseDelegate<T>)Delegate.CreateDelegate(typeof(ParseDelegate<T>), typeof(T), "Parse", true);
				ParseDelegateStore<T>.Parse = parse;
			}
			return parse(s);
		}

		public static bool TryParse<T>(string s, out T result)
		{
			TryParseDelegate<T> tryParse = ParseDelegateStore<T>.TryParse;
			if (tryParse == null)
			{
				tryParse = (TryParseDelegate<T>)Delegate.CreateDelegate(typeof(TryParseDelegate<T>), typeof(T), "TryParse", true);
				ParseDelegateStore<T>.TryParse = tryParse;
			}
			return tryParse(s, out result);
		}
	}
}
