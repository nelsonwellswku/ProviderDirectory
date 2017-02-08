using System;
using FluentAssertions;
using FluentValidation;
using Marten;
using MediatR;
using NUnit.Framework;
using Octogami.ProviderDirectory.Application.Domain;
using Octogami.ProviderDirectory.Application.Feature.CreateProvider;
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

			// TODO: Once implemented, use get provider query to assert that everything was saved correctly
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