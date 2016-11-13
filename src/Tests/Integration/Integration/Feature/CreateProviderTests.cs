using System;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NUnit.Framework;
using Octogami.ProviderDirectory.Application.Feature.CreateProvider;
using Octogami.ProviderDirectory.Tests.Integration.TestSupport;

namespace Octogami.ProviderDirectory.Tests.Integration.Feature
{
	public class CreateProviderTests
	{
		[Test]
		public void CanCreateProvider_HappyPath()
		{
			// Arrange
			var testContainer = new TestContainer().GetContainer();
			var mediator = testContainer.GetInstance<IMediator>();

			// Act
			var command = ValidCommand;
			var response = mediator.Send(command);

			// Assert
			Assert.AreNotEqual(default(Guid), response.ProviderId);

			// TODO: Once implemented, use get provider query to assert that everything was saved correctly
		}

		[Test]
		public void MissingNPI_FailsValidation()
		{
			// Arrange
			var container = new TestContainer().GetContainer();
			var mediator = container.GetInstance<IMediator>();
			var command = ValidCommand;
			command.NPI = string.Empty;

			// Act
			Action act = () => mediator.Send(command);

			// Assert
			act.ShouldThrow<ValidationException>();
		}

		[Test]
		public void MissingFirstName_FailsValidation()
		{
			// Arrange
			var container = new TestContainer().GetContainer();
			var mediator = container.GetInstance<IMediator>();
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
			var container = new TestContainer().GetContainer();
			var mediator = container.GetInstance<IMediator>();
			var command = ValidCommand;
			command.LastName = string.Empty;

			// Act
			Action act = () => mediator.Send(command);

			// Assert
			act.ShouldThrow<ValidationException>();
		}

		private CreateProviderCommand ValidCommand => new CreateProviderCommand
		{
			NPI = "ABC123",
			FirstName = "John",
			LastName = "Smith",
			AddressLineOne = "100 Old Hickory Blvd.",
			AddressLineTwo = "Apt A.",
			State = "TN",
			City = "Nashville",
			Zip = "37200"
		};
	}
}