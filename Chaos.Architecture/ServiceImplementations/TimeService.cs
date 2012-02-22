using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chaos.Architecture.Services;

namespace Chaos.Architecture.ServiceImplementations
{
	public sealed class TimeService : ITimeService
	{
		private readonly Func<DateTime> _getUtcNow;

		public DateTime UtcNow
		{
			get
			{
				DateTime result = _getUtcNow();
				if (result.Kind != DateTimeKind.Utc)
					throw new InvalidOperationException("The getUtcNow delegate supplied to the TimeService returned a non UTC time.");
				return result;
			}
		}

		private static readonly TimeService _default = new TimeService();

		public static ITimeService Default { get { return _default; } }
		public static ITimeService FromDelegate(Func<DateTime> getUtcNow)
		{
			return new TimeService(getUtcNow);
		}

		private TimeService()
		{
			_getUtcNow = () => DateTime.UtcNow;
		}

		private TimeService(Func<DateTime> getUtcNow)
		{
			_getUtcNow = getUtcNow;
		}
	}
}
