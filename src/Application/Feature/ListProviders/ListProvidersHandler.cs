using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Marten;
using Marten.Linq;
using MediatR;
using Octogami.ProviderDirectory.Application.Domain;
using Octogami.ProviderDirectory.Application.Feature.Common;

namespace Octogami.ProviderDirectory.Application.Feature.ListProviders
{
	public class ListProvidersQuery : IRequest<IPaged<ProviderResponse>>
	{
		public ListProvidersQuery()
		{
			Page = 1;
			RecordsPerPage = 10;
		}

		public int Page { get; set; }
		public int RecordsPerPage { get; set; }
	}

	public class ListProvidersQueryValidator : AbstractValidator<ListProvidersQuery>
	{
		public ListProvidersQueryValidator()
		{
			RuleFor(x => x.Page).GreaterThanOrEqualTo(1).LessThan(int.MaxValue);
			RuleFor(x => x.RecordsPerPage).LessThan(100);
		}
	}

	public class ListProvidersQueryHandler : IRequestHandler<ListProvidersQuery, IPaged<ProviderResponse>>
	{
		private readonly IDocumentSession _session;

		public ListProvidersQueryHandler(IDocumentSession session)
		{
			_session = session;
		}

		public IPaged<ProviderResponse> Handle(ListProvidersQuery message)
		{
			QueryStatistics stats;
			var results = _session.Query<Provider>()
				.Stats(out stats)
				.Skip((message.Page - 1) * message.RecordsPerPage)
				.Take(message.RecordsPerPage)
				.ToList();

			return new Paged<ProviderResponse>(results.Select(x => new ProviderResponse
			{
				ProviderId = x.ProviderId,
				NPI = x.NPI,
				FirstName = x.FirstName,
				LastName = x.LastName
			}), stats.TotalResults, message.Page, message.RecordsPerPage);
		}
	}
}