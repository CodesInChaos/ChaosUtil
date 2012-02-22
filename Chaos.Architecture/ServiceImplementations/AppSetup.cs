using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Chaos.Architecture.Services;
using System.Reflection;

namespace Chaos.Architecture.ServiceImplementations
{
	public class AppSetup : IAppSetup
	{
		private string _appDataDirectory;
		private string _userDataDirectory;
		private string _documentDirectory;

		public string UserDataDirectory
		{
			get
			{
				if (_userDataDirectory == null)
					throw new InvalidOperationException("No UserDataDirectory set or found");
				return _userDataDirectory;
			}
		}

		public string AppDataDirectory
		{
			get
			{
				if (_appDataDirectory == null)
					throw new InvalidOperationException("No AppDataDirectory set or found");
				return _appDataDirectory;
			}
		}

		public string DocumentDirectory
		{
			get
			{
				if (_documentDirectory == null)
					throw new InvalidOperationException("No DocumentDirectory set or found");
				return _documentDirectory;
			}
			set
			{
				_documentDirectory = value;
			}
		}

		private Assembly _mainAssembly;

		protected virtual string AppCodeDirectory()
		{
			string filePath;
			if (_mainAssembly != null)
				filePath = _mainAssembly.Location;
			else
				filePath = Environment.GetCommandLineArgs()[0];
			return Path.GetDirectoryName(filePath);
		}

		protected virtual string FindAppDataDirectory()
		{
			string exeDir = AppCodeDirectory();
			string dataRootDir = exeDir;
			while (dataRootDir != null)
			{
				string dataDir = dataRootDir + "\\data\\";
				if (Directory.Exists(dataDir))
					return dataDir;
				dataRootDir = Path.GetDirectoryName(dataRootDir);
			}
			return null;
		}

		public AppSetup()
		{
			_appDataDirectory = FindAppDataDirectory();
		}

		public AppSetup(Assembly mainAssembly)
		{
			_mainAssembly = mainAssembly;
			_appDataDirectory = FindAppDataDirectory();
		}
	}
}
