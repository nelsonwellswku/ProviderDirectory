using System.Globalization;
using System.Linq;
using Bogus;
using FluentAssertions;
using Marten;
using MediatR;
using NUnit.Framework;
using Octogami.ProviderDirectory.Application.Domain;
using Octogami.ProviderDirectory.Application.Feature.CreateProvider;
using Octogami.ProviderDirectory.Application.Feature.ListProviders;
using Octogami.ProviderDirectory.Tests.Integration.TestSupport;
using StructureMap;
using Address = Octogami.ProviderDirectory.Application.Feature.CreateProvider.Address;

namespace Octogami.ProviderDirectory.Tests.Integration.Feature
{
	public class ListProviderTests
	{
		private IContainer _container;

		[OneTimeSetUp]
		public void SetUpFixture()
		{
			var container = TestContainerFactory.New();
			var documentStore = container.GetInstance<IDocumentStore>();
			documentStore.Advanced.PrecompileAllStorage();
			documentStore.Advanced.Clean.DeleteDocumentsFor(typeof(Provider));

			// Insert 10 random documents
			var createProvider = container.GetInstance<CreateProviderHandler>();

			var commands = new Faker<CreateProviderCommand>()
				.RuleFor(cmd => cmd.FirstName, f => f.Name.FirstName())
				.RuleFor(cmd => cmd.LastName, f => f.Name.LastName())
				.RuleFor(cmd => cmd.NPI, f => new Randomizer().Replace("#######"))
				.RuleForType(typeof(Address), f =>
				{
					var address = f.Address;
					return new Address
					{
						StreetOne = address.StreetAddress(true),
						City = address.City(),
						State = address.State(),
						Zip = address.ZipCode()
					};
				})
				.RuleFor(x => x.Gender, f => "male")
				.RuleFor(x => x.EntityType, f => "individual")
				.RuleFor(x => x.EnumerationDate, f => f.Date.Recent().ToString(CultureInfo.InvariantCulture));

			foreach (var num in Enumerable.Range(0, 10))
			{
				var command = commands.Generate();
				command.FirstName = command.FirstName + num;
				createProvider.Handle(command);
			}
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
		public void ListProviders_HappyPath()
		{
			// Arrange
			var mediator = _container.GetInstance<Mediator>();

			// Act
			var result = mediator.Send(new ListProvidersQuery());

			// Assert
			result.CurrentPage.Should().Be(1);
			result.CurrentRecordsPerPage.Should().Be(10);
			result.TotalItems.Should().Be(10);
			result.Items.Count().Should().Be(10);
		}

		[Test]
		public void ListProviders_SimplestPaging()
		{
			// Arrange
			var mediator = _container.GetInstance<Mediator>();

			// Act
			var resultOne = mediator.Send(new ListProvidersQuery
			{
				Page = 1,
				RecordsPerPage = 2
			});

			var resultTwo = mediator.Send(new ListProvidersQuery
			{
				Page = 2,
				RecordsPerPage = 2
			});

			// Assert
			resultOne.Items.Select(x => int.Parse(x.FirstName.Last().ToString())).ShouldBeEquivalentTo(new[] {0, 1});
			resultTwo.Items.Select(x => int.Parse(x.FirstName.Last().ToString())).ShouldBeEquivalentTo(new[] {2, 3});
		}
	}
}