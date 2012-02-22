using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Architecture.Services
{
	/// <summary>
	/// A service that gives access to the current time
	/// </summary>
	public interface ITimeService
	{
		DateTime UtcNow { get; }
	}
}
