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
	public class ListProvidersQuery : IRequest<ListProvidersResponse>
	{
		public ListProvidersQuery()
		{
			Page = 1;
			RecordsPerPage = 10;
		}

		public int Page { get; set; }
		public int RecordsPerPage { get; set; }
	}

	public class ListProvidersResponse
	{
		public IEnumerable<ProviderResponse> Providers { get; set; }
		public int TotalPages { get; set; }
	}

	public class ListProvidersQueryValidator : AbstractValidator<ListProvidersQuery>
	{
	}

	public class ListProvidersQueryHandler : IRequestHandler<ListProvidersQuery, ListProvidersResponse>
	{
		private readonly IDocumentSession _session;

		public ListProvidersQueryHandler(IDocumentSession session)
		{
			_session = session;
		}

		public ListProvidersResponse Handle(ListProvidersQuery message)
		{
			QueryStatistics stats;
			var results = _session.Query<Provider>()
				.Stats(out stats)
				.Skip((message.Page - 1) * message.RecordsPerPage)
				.Take(message.RecordsPerPage)
				.ToList();

			return new ListProvidersResponse
			{
				TotalPages = (int) Math.Ceiling(stats.TotalResults / (double) message.RecordsPerPage),
				Providers = results.Select(x => new ProviderResponse
				{
					ProviderId = x.ProviderId,
					NPI = x.NPI,
					FirstName = x.FirstName,
					LastName = x.LastName
				})
			};
		}
	}
}