using System.Configuration;
using System.IO;
using System.Linq;
using CsvHelper;

namespace Octogami.ProviderDirectory.NpiDataProcessor
{
	class Program
	{
		static void Main(string[] args)
		{
			var npiFilePath = ConfigurationManager.AppSettings["npiFilePath"];
			using (var fileStream = new FileStream(npiFilePath, FileMode.Open))
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
