using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using MediatR;
using Octogami.ProviderDirectory.Application.Feature.CreateProvider;

namespace Octogami.ProviderDirectory.NpiDataProcessor
{
	public class NpiProcessorRoot
	{
		private readonly ApplicationConfiguration _configuration;
		private readonly IMediator _mediator;

		public NpiProcessorRoot(ApplicationConfiguration configuration, IMediator mediator)
		{
			_configuration = configuration;
			_mediator = mediator;
		}

		public void Process()
		{
			using (var fileStream = new FileStream(_configuration.NpiFilePath, FileMode.Open))
			using (var fileReader = new StreamReader(fileStream))
			using (var csvReader = new CsvReader(fileReader))
			{
				csvReader.Configuration.Delimiter = ",";
				csvReader.Configuration.HasHeaderRecord = false;
				var createCommands = csvReader.GetRecords<NpiRow>()
					.Skip(1)
					.Where(x => x.EntityTypeCode == "1")
					.Take(10000)
					.Select(ToCreateProviderCommand);


				foreach (var command in createCommands)
				{
					_mediator.Send(command);
				}
			}
		}

		private CreateProviderCommand ToCreateProviderCommand(NpiRow npiRow)
		{
			return new CreateProviderCommand
			{
				EntityType = MapEntityType(npiRow.EntityTypeCode),
				EnumerationDate = npiRow.ProviderEnumerationDate,
				FirstName = npiRow.ProviderFirstName,
				Gender = MapGender(npiRow.ProviderGenderCode),
				LastName = npiRow.ProviderLastNameLegalName,
				MailingAddress = new Address
				{
					StreetOne = npiRow.ProviderFirstLineBusinessMailingAddress,
					StreetTwo = npiRow.ProviderSecondLineBusinessMailingAddress,
					City = npiRow.ProviderBusinessMailingAddressCityName,
					State = npiRow.ProviderBusinessMailingAddressStateName,
					Zip = npiRow.ProviderBusinessMailingAddressPostalCode
				},
				MiddleName = npiRow.ProviderMiddleName,
				PracticeAddress = new Address
				{
					StreetOne = npiRow.ProviderFirstLineBusinessPracticeLocationAddress,
					StreetTwo = npiRow.ProviderSecondLineBusinessPracticeLocationAddress,
					City = npiRow.ProviderBusinessPracticeLocationAddressCityName,
					State = npiRow.ProviderBusinessPracticeLocationAddressStateName,
					Zip = npiRow.ProviderBusinessPracticeLocationAddressPostalCode
				},
				NPI = npiRow.NPI
			};
		}

		private string MapGender(string gender)
		{
			if (gender.Equals("m", StringComparison.InvariantCultureIgnoreCase))
			{
				return "male";
			}

			if (gender.Equals("f", StringComparison.InvariantCultureIgnoreCase))
			{
				return "female";
			}

			return null;
		}

		private string MapEntityType(string entityType)
		{
			if (entityType == "1")
			{
				return "individual";
			}

			if (entityType == "2")
			{
				return "organization";
			}

			return null;
		}
	}
}
