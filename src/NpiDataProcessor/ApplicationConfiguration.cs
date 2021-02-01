using System.Configuration;

namespace Octogami.ProviderDirectory.NpiDataProcessor
{
	public class ApplicationConfiguration
	{
		public string NpiFilePath => ConfigurationManager.AppSettings["NpiFilePath"];

		public string TaxonomyFilePath => ConfigurationManager.AppSettings["TaxonomyFilePath"];

		public string ConnectionString => ConfigurationManager.AppSettings["ConnectionString"];

		public string MaxRecordsToImport => ConfigurationManager.AppSettings["MaxRecordsToImport"];
	}
}