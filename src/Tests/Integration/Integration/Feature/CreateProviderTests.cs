using System;
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
			var command = new CreateProviderCommand
			{
				NPI = "555ABC",
				FirstName = "John",
				LastName = "Smith"
			};
			var response = mediator.Send(command);

			// Assert
			Assert.AreNotEqual(default(Guid), response.ProviderId);

			// TODO: Once implemented, use get provider query to assert that everything was saved correctly
		}
	}
}