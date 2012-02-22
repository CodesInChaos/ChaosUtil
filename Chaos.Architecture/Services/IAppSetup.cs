using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaos.Architecture.Services
{
	public interface IAppSetup
	{
		/// <summary>
		/// Where to store hidden userrelated data. Assumed to be writable
		/// </summary>
		string UserDataDirectory { get; }
		/// <summary>
		/// Where the application data is, readonly
		/// </summary>
		string AppDataDirectory { get; }
		/// <summary>
		/// Where the user wants to store his documents
		/// </summary>
		string DocumentDirectory { get; set; }
	}
}
