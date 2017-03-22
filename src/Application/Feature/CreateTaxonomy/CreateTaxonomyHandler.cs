using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

	public class CreateTaxonomyHandler : IRequestHandler<CreateTaxonomyCommand, CreateTaxonomyResponse>
	{
		private readonly IDocumentSession _session;


		public CreateTaxonomyHandler(IDocumentSession session)
		{
			_session = session;
		}

		public CreateTaxonomyResponse Handle(CreateTaxonomyCommand message)
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
			_session.SaveChanges();

			return new CreateTaxonomyResponse
			{
				TaxonomyId = taxonomy.TaxonomyId
			};
		}
	}
}
