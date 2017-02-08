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
				LastName = provider.LastName,
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
	}
}