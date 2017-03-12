using System.Configuration;

namespace Octogami.ProviderDirectory.Application
{
	public class ApplicationConfiguration
	{
		public string ConnectionString => ConfigurationManager.AppSettings["ConnectionString"];
	}
}