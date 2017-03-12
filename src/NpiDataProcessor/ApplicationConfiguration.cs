using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octogami.ProviderDirectory.NpiDataProcessor
{
	public class ApplicationConfiguration
	{
		public string NpiFilePath => ConfigurationManager.AppSettings["npiFilePath"];
	}
}
