using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace Octogami.ProviderDirectory.NpiDataProcessor
{
	public class NpiProcessorRoot
	{
		private readonly ApplicationConfiguration _configuration;

		public NpiProcessorRoot(ApplicationConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void Process()
		{
			using (var fileStream = new FileStream(_configuration.NpiFilePath, FileMode.Open))
			using (var fileReader = new StreamReader(fileStream))
			using (var csvReader = new CsvReader(fileReader))
			{
				csvReader.Configuration.Delimiter = ",";
				csvReader.Configuration.HasHeaderRecord = false;
				var records = csvReader.GetRecords<NpiRow>().Take(10000);
				var tenThousandRecords = records.ToList();
			}
		}
	}
}
