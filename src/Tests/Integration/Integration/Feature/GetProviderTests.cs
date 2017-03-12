using System;
using System.Globalization;
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
	public class GetProviderTests
	{
		private IContainer _container;

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
		public void GetProvider_HappyPath()
		{
			// Arrange
			var mediator = _container.GetInstance<IMediator>();
			var providerId = mediator.Send(new CreateProviderCommand
			{
				NPI = "123",
				FirstName = "Elijah",
				LastName = "Smith",
				Gender = "male",
				EntityType = "individual",
				EnumerationDate = DateTime.Now.ToString(CultureInfo.InvariantCulture),
				MailingAddress = new Address(),
				PracticeAddress = new Address()
			}).ProviderId;

			// Act
			var result = mediator.Send(new GetProviderQuery {ProviderId = providerId});

			// Assert
			result.ProviderId.Should().Be(providerId);
			result.NPI.Should().Be("123");
			result.FirstName.Should().Be("Elijah");
			result.LastName.Should().Be("Smith");
		}

		[Test]
		public void GetProvider_ProviderNotFound()
		{
			// Arrange
			var mediator = _container.GetInstance<IMediator>();

			// Act
			Action act = () => mediator.Send(new GetProviderQuery {ProviderId = Guid.NewGuid()});

			// Assert
			act.ShouldThrow<ValidationException>();
		}
	}
}