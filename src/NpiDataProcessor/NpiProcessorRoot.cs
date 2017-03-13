﻿using System;
using System.IO;
using System.Linq;
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

		private static int HeaderRow => 1;

		private int MaxRecordsToImport => int.Parse(_configuration.MaxRecordsToImport);

		public void Process()
		{
			using (var fileStream = new FileStream(_configuration.NpiFilePath, FileMode.Open))
			using (var fileReader = new StreamReader(fileStream))
			using (var csvReader = new CsvReader(fileReader))
			{
				csvReader.Configuration.Delimiter = ",";
				csvReader.Configuration.HasHeaderRecord = false;
				var createCommands = csvReader.GetRecords<NpiRow>()
					.Skip(HeaderRow)
					.Where(IsIndividual)
					.Take(MaxRecordsToImport)
					.Select(ToCreateProviderCommand);

				foreach (var command in createCommands)
				{
					_mediator.Send(command);
				}
			}
		}

		private static bool IsIndividual(NpiRow row)
		{
			return row.EntityTypeCode == "1";
		}

		private static CreateProviderCommand ToCreateProviderCommand(NpiRow npiRow)
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

		private static string MapGender(string gender)
		{
			if (gender.Equals("m", StringComparison.InvariantCultureIgnoreCase))
				return "male";

			if (gender.Equals("f", StringComparison.InvariantCultureIgnoreCase))
				return "female";

			return null;
		}

		private static string MapEntityType(string entityType)
		{
			if (entityType == "1")
				return "individual";

			if (entityType == "2")
				return "organization";

			return null;
		}
	}
}