using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Chaos.Architecture
{
	public class ValidationResults
	{
		private List<string> _errors = new List<string>();

		public bool HasErrors { get { return !IsSuccess; } }
		public bool IsSuccess { get { return _errors.Count == 0; } }

		public void AddError<T>(Expression<Func<T>> exp, string error)
		{
			_errors.Add(error);
		}

		public void AddError(string error)
		{
			_errors.Add(error);
		}
	}
}
