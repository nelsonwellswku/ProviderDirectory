using System;
using System.Linq;
using FluentValidation;
using Marten;
using MediatR;
using Octogami.ProviderDirectory.Application.Domain;

namespace Octogami.ProviderDirectory.Application.Feature.GetProvider
{
	public class GetProviderQuery : IRequest<GetProviderResponse>
	{
		public Guid ProviderId { get; set; }
	}

	public class GetProviderResponse
	{
		public Guid ProviderId { get; set; }
		public string NPI { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
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

	public class GetProviderHandler : IRequestHandler<GetProviderQuery, GetProviderResponse>
	{
		private readonly IDocumentSession _session;

		public GetProviderHandler(IDocumentSession session)
		{
			_session = session;
		}

		public GetProviderResponse Handle(GetProviderQuery message)
		{
			var provider = _session.Load<Provider>(message.ProviderId);
			return new GetProviderResponse
			{
				ProviderId = provider.ProviderId,
				NPI = provider.NPI,
				FirstName = provider.FirstName,
				LastName = provider.LastName
			};
		}
	}
}