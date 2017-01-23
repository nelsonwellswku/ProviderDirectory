using System;
using System.Linq;
using FluentValidation;
using Marten;
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

	public class GetProviderHandler : IRequestHandler<GetProviderQuery, ProviderResponse>
	{
		private readonly IDocumentSession _session;

		public GetProviderHandler(IDocumentSession session)
		{
			_session = session;
		}

		public ProviderResponse Handle(GetProviderQuery message)
		{
			var provider = _session.Load<Provider>(message.ProviderId);
			return new ProviderResponse
			{
				ProviderId = provider.ProviderId,
				NPI = provider.NPI,
				FirstName = provider.FirstName,
				LastName = provider.LastName
			};
		}
	}
}