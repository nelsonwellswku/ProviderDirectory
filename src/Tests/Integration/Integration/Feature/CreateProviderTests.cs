using System;
using System.Linq;
using FluentAssertions;
using FluentValidation;
using Marten;
using MediatR;
using NUnit.Framework;
using Octogami.ProviderDirectory.Application.Domain;
using Octogami.ProviderDirectory.Application.Feature.CreateProvider;
using Octogami.ProviderDirectory.Application.Feature.GetProvider;
using Octogami.ProviderDirectory.Tests.Integration.TestSupport;
using StructureMap;
using Address = Octogami.ProviderDirectory.Application.Feature.CreateProvider.Address;

namespace Octogami.ProviderDirectory.Tests.Integration.Feature
{
	public class CreateProviderTests
	{
		private IContainer _container;

		private static CreateProviderCommand ValidCommand => new CreateProviderCommand
		{
			NPI = "ABC123",
			EntityType = "individual",
			EnumerationDate = "10/31/2015",
			FirstName = "John",
			LastName = "Smith",
			Gender = "male",
			MailingAddress = new Address
			{
				StreetOne = "100 Old Hickory Blvd.",
				StreetTwo = "Suite 250.",
				State = "TN",
				City = "Nashville",
				Zip = "37200"
			},
			PracticeAddress = new Address
			{
				StreetOne = "425 Peachtree Ave",
				StreetTwo = "Suite 1000",
				State = "TN",
				City = "Nashville",
				Zip = "37211"
			}
		};

		[OneTimeSetUp]
		public void SetUpFixture()
		{
			var container = TestContainerFactory.New();
			var documentStore = container.GetInstance<IDocumentStore>();
			documentStore.Advanced.PrecompileAllStorage();
			documentStore.Advanced.Clean.DeleteDocumentsFor(typeof(Provider));
		}

		[SetUp]
		public void SetUp()
		{
			_container = TestContainerFactory.New();
		}

		[TearDown]
		public void TearDown()
		{
			_container.Dispose();
		}

		[Test]
		public void CanCreateProvider_HappyPath()
		{
			// Arrange
			var mediator = _container.GetInstance<IMediator>();
			var command = ValidCommand;

			// Act
			var response = mediator.Send(command);

			// Assert
			Assert.AreNotEqual(default(Guid), response.ProviderId);

			var createdProvider =
				_container.GetInstance<IDocumentSession>()
					.Query<Provider>()
					.First(x => x.ProviderId == response.ProviderId);

			createdProvider.NPI.Should().Be(ValidCommand.NPI);
			createdProvider.FirstName.Should().Be(ValidCommand.FirstName);
			createdProvider.LastName.Should().Be(ValidCommand.LastName);

			createdProvider.MailingAddress.StreetOne.Should().Be(ValidCommand.MailingAddress.StreetOne);
			createdProvider.MailingAddress.StreetTwo.Should().Be(ValidCommand.MailingAddress.StreetTwo);
			createdProvider.MailingAddress.City.Should().Be(ValidCommand.MailingAddress.City);
			createdProvider.MailingAddress.State.Abbreviation.Should().Be("TN");
			createdProvider.MailingAddress.State.Name.Should().Be("Tennessee");
			createdProvider.MailingAddress.Zip.Should().Be(ValidCommand.MailingAddress.Zip);

			createdProvider.PracticeAddress.StreetOne.Should().Be(ValidCommand.PracticeAddress.StreetOne);
			createdProvider.PracticeAddress.StreetTwo.Should().Be(ValidCommand.PracticeAddress.StreetTwo);
			createdProvider.PracticeAddress.City.Should().Be(ValidCommand.PracticeAddress.City);
			createdProvider.PracticeAddress.State.Abbreviation.Should().Be("TN");
			createdProvider.PracticeAddress.State.Name.Should().Be("Tennessee");
			createdProvider.PracticeAddress.Zip.Should().Be(ValidCommand.PracticeAddress.Zip);
		}

		[Test]
		public void MissingNPI_FailsValidation()
		{
			// Arrange
			var mediator = _container.GetInstance<IMediator>();
			var command = ValidCommand;
			command.NPI = string.Empty;

			// Act
			Action act = () => mediator.Send(command);

			// Assert
			act.ShouldThrow<ValidationException>();
		}

		[Test]
		public void DuplicateNPI_FailsValidation()
		{
			// Arrange
			var mediator = _container.GetInstance<IMediator>();
			var command = ValidCommand;
			command.NPI = "QWERTY";

			// Act
			mediator.Send(command);
			Action act = () => mediator.Send(command);

			// Assert
			act.ShouldThrow<ValidationException>();
		}

		[Test]
		public void MissingFirstName_FailsValidation()
		{
			// Arrange
			var mediator = _container.GetInstance<IMediator>();
			var command = ValidCommand;
			command.FirstName = string.Empty;

			// Act
			Action act = () => mediator.Send(command);

			// Assert
			act.ShouldThrow<ValidationException>();
		}

		[Test]
		public void MissingLastName_FailsValidation()
		{
			// Arrange
			var mediator = _container.GetInstance<IMediator>();
			var command = ValidCommand;
			command.LastName = string.Empty;

			// Act
			Action act = () => mediator.Send(command);

			// Assert
			act.ShouldThrow<ValidationException>();
		}
	}
}