using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Marten;
using MediatR;
using Octogami.ProviderDirectory.Application.Domain;

namespace Octogami.ProviderDirectory.Application.Feature.CreateTaxonomy
{
	public class CreateTaxonomyCommand : IRequest<CreateTaxonomyResponse>
	{
		public string TaxonomyCode { get; set; }

		public string Grouping { get; set; }

		public string Classification { get; set; }

		public string Specialization { get; set; }

		public string Definition { get; set; }

		public string Notes { get; set; }
	}

	public class CreateTaxonomyResponse
	{
		public Guid TaxonomyId { get; set; }
	}

	public class CreateTaxonomyValidator : AbstractValidator<CreateTaxonomyCommand>
	{
		private readonly IDocumentSession _documentSession;

		public CreateTaxonomyValidator(IDocumentSession documentSession)
		{
			_documentSession = documentSession;

			RuleFor(x => x.TaxonomyCode).Must(BeUnique).WithMessage("Command has a duplicate taxonomy code.");
		}

		private bool BeUnique(string taxonomyCode)
		{
			return ! _documentSession.Query<Taxonomy>().Any(x => x.TaxonomyCode == taxonomyCode);
		}
	}

	public class CreateTaxonomyHandler : ICancellableAsyncRequestHandler<CreateTaxonomyCommand, CreateTaxonomyResponse>
	{
		private readonly IDocumentSession _session;


		public CreateTaxonomyHandler(IDocumentSession session)
		{
			_session = session;
		}

		public async Task<CreateTaxonomyResponse> Handle(CreateTaxonomyCommand message, CancellationToken cancellationToken)
		{
			var taxonomy = new Taxonomy
			{
				TaxonomyCode = message.TaxonomyCode,
				Classification = message.Classification,
				Definition = message.Definition,
				Grouping = message.Grouping,
				Notes = message.Notes,
				Specialization = message.Specialization
			};

			_session.Store(taxonomy);
			await _session.SaveChangesAsync(cancellationToken);

			return new CreateTaxonomyResponse
			{
				TaxonomyId = taxonomy.TaxonomyId
			};
		}
	}
}
