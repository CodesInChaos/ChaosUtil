using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Architecture
{
	public class LocalizableString
	{
		private Dictionary<string, string> _values = new Dictionary<string, string>();

		public LocalizableString(IEnumerable<KeyValuePair<string, string>> values)
		{
			foreach (var pair in values)
				_values.Add(pair.Key, pair.Value);
		}

		public string this[string lang]
		{
			get
			{
				string result;
				if (_values.TryGetValue(lang, out result))
					return result;
				else
					return _values["en"];
			}
		}

		public override string ToString()
		{
			return "[[" + this["en"] + "]]";
		}
	}
}
