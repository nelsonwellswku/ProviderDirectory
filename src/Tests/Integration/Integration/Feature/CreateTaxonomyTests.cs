using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Marten;
using MediatR;
using NUnit.Framework;
using Octogami.ProviderDirectory.Application.Domain;
using Octogami.ProviderDirectory.Application.Feature.CreateTaxonomy;
using Octogami.ProviderDirectory.Tests.Integration.TestSupport;
using StructureMap;

namespace Octogami.ProviderDirectory.Tests.Integration.Feature
{
	public class CreateTaxonomyTests
	{
		private IContainer _container;

		[OneTimeSetUp]
		public void SetUpFixture()
		{
			var container = TestContainerFactory.New();
			var documentStore = container.GetInstance<IDocumentStore>();
			documentStore.Advanced.PrecompileAllStorage();
			documentStore.Advanced.Clean.DeleteDocumentsFor(typeof(Taxonomy));
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
		public async Task CanCreateNewTaxonomy()
		{
			// Arrange
			var mediator = _container.GetInstance<IMediator>();
			var command = new CreateTaxonomyCommand
			{
				Classification = "Classification",
				Definition = "Definition",
				Grouping = "Grouping",
				Notes = "Notes",
				Specialization = "Specialization",
				TaxonomyCode = "TaxonomyCode"
			};

			// Act
			var result = await mediator.Send(command);

			// Assert
			result.TaxonomyId.Should().NotBeEmpty();

			var retrievedTaxonomy = _container.GetInstance<IDocumentSession>()
				.Query<Taxonomy>()
				.Single(x => x.TaxonomyId == result.TaxonomyId);

			retrievedTaxonomy.Classification.Should().Be(command.Classification);
			retrievedTaxonomy.Definition.Should().Be(command.Definition);
			retrievedTaxonomy.Grouping.Should().Be(command.Grouping);
			retrievedTaxonomy.Notes.Should().Be(command.Notes);
			retrievedTaxonomy.Specialization.Should().Be(command.Specialization);
			retrievedTaxonomy.TaxonomyCode.Should().Be(command.TaxonomyCode);
		}

		[Test]
		public void CanNotCreateTaxonomiesWithDuplicateCodes()
		{
			// Arrange
			var mediator = _container.GetInstance<IMediator>();
			var command = new CreateTaxonomyCommand {TaxonomyCode = "ABC"};
			mediator.Send(command);

			// Act
			Action act = () => mediator.Send(command);

			// Assert
			act.ShouldThrow<ValidationException>().And.Message.Contains("duplicate taxonomy code").Should().BeTrue();
		}
	}
}