using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Marten;
using Marten.Services.Includes;
using MediatR;
using Octogami.ProviderDirectory.Application.Domain;
using Octogami.ProviderDirectory.Application.Feature.Common;

namespace Octogami.ProviderDirectory.Application.Feature.GetProvider
{
	public class GetProviderQuery : IRequest<ProviderResponse>
	{
		public Guid ProviderId { get; set; }
	}

	public class GetProviderValidator : AbstractValidator<GetProviderQuery>
	{
		private readonly IDocumentSession _session;

		public GetProviderValidator(IDocumentSession session)
		{
			_session = session;
			RuleFor(x => x.ProviderId).Must(Exist);
		}

		private bool Exist(Guid providerId)
		{
			return _session.Query<Provider>().Any(x => x.ProviderId == providerId);
		}
	}

	public class GetProviderHandler : ICancellableAsyncRequestHandler<GetProviderQuery, ProviderResponse>
	{
		private readonly IDocumentSession _session;

		public GetProviderHandler(IDocumentSession session)
		{
			_session = session;
		}

		public async Task<ProviderResponse> Handle(GetProviderQuery message, CancellationToken cancellationToken)
		{
			Taxonomy primaryTaxonomy = null;
			var provider = await _session.Query<Provider>()
					.Include<Taxonomy>(p => p.PrimaryTaxonomyId, t => primaryTaxonomy = t, JoinType.LeftOuter)
					.SingleAsync(x => x.ProviderId == message.ProviderId, cancellationToken);

			return new ProviderResponse
			{
				ProviderId = provider.ProviderId,
				NPI = provider.NPI,
				FirstName = provider.FirstName,
				LastName = provider.LastName,
				PrimaryTaxonomy = primaryTaxonomy == null ? new TaxonomyResponse() : Map(primaryTaxonomy),
				MailingAddress = new Common.Address
				{
					StreetOne = provider.MailingAddress.StreetOne,
					StreetTwo = provider.MailingAddress.StreetTwo,
					City = provider.MailingAddress.City,
					State = new Common.State
					{
						Abbreviation = provider.MailingAddress.State.Abbreviation,
						Name = provider.MailingAddress.State.Name
					},
					Zip = provider.MailingAddress.Zip
				},
				PracticeAddress = new Common.Address
				{
					StreetOne = provider.PracticeAddress.StreetOne,
					StreetTwo = provider.PracticeAddress.StreetTwo,
					City = provider.PracticeAddress.City,
					State = new Common.State
					{
						Abbreviation = provider.PracticeAddress.State.Abbreviation,
						Name = provider.PracticeAddress.State.Name
					},
					Zip = provider.PracticeAddress.Zip
				}
			};
		}

		private static TaxonomyResponse Map(Taxonomy taxonomy)
		{
			return new TaxonomyResponse
			{
				TaxonomyCode = taxonomy.TaxonomyCode,
				TaxonomyId = taxonomy.TaxonomyId,
				Definition = taxonomy.Definition,
				Notes = taxonomy.Notes,
				Grouping = taxonomy.Grouping,
				Specialization = taxonomy.Specialization,
				Classification = taxonomy.Classification
			};
		}
	}
}