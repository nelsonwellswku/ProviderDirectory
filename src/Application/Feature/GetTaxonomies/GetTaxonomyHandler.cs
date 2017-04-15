using System;
using System.Linq;
using Marten;
using Marten.Linq;
using MediatR;
using Octogami.ProviderDirectory.Application.Domain;
using Octogami.ProviderDirectory.Application.Feature.Common;

namespace Octogami.ProviderDirectory.Application.Feature.GetTaxonomies
{
	public class GetTaxonomiesQuery : IRequest<IPaged<TaxonomyResponse>>
	{
		public GetTaxonomiesQuery()
		{
			Page = 1;
			RecordsPerPage = 1000;
		}

		public int Page { get; set; }

		public int RecordsPerPage { get; set; }
	}

	public class GetTaxonomiesHandler : IRequestHandler<GetTaxonomiesQuery, IPaged<TaxonomyResponse>>
	{
		private readonly IDocumentSession _session;

		public GetTaxonomiesHandler(IDocumentSession session)
		{
			_session = session;
		}

		public IPaged<TaxonomyResponse> Handle(GetTaxonomiesQuery message)
		{
			QueryStatistics stats;
			var results = _session.Query<Taxonomy>()
				.Stats(out stats)
				.Skip((message.Page - 1) * message.RecordsPerPage)
				.Take(message.RecordsPerPage)
				.ToList();

			return new Paged<TaxonomyResponse>(results.Select(MapToResponse), stats.TotalResults, message.Page, message.RecordsPerPage);
		}

		private static TaxonomyResponse MapToResponse(Taxonomy taxonomy)
		{
			return new TaxonomyResponse
			{
				TaxonomyId = taxonomy.TaxonomyId,
				TaxonomyCode = taxonomy.TaxonomyCode,
				Definition = taxonomy.Definition,
				Notes = taxonomy.Notes,
				Grouping = taxonomy.Grouping,
				Specialization = taxonomy.Specialization,
				Classification = taxonomy.Classification
			};
		}
	}
}
