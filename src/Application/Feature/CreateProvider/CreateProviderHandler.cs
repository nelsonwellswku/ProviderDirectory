using System;
using System.Linq;
using FluentValidation;
using Marten;
using MediatR;
using Octogami.ProviderDirectory.Application.Domain;

namespace Octogami.ProviderDirectory.Application.Feature.CreateProvider
{
	public class CreateProviderCommand : IRequest<CreateProviderResponse>
	{
		public string NPI { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string AddressLineOne { get; set; }
		public string AddressLineTwo { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
	}

	public class CreateProviderResponse
	{
		public Guid ProviderId { get; set; }
	}

	public class CreateProviderValidator : AbstractValidator<CreateProviderCommand>
	{
		private readonly IDocumentSession _session;

		public CreateProviderValidator(IDocumentSession session)
		{
			_session = session;

			RuleFor(x => x).NotNull();
			RuleFor(x => x.NPI).NotEmpty().WithMessage("NPI should not be empty");
			RuleFor(x => x.FirstName).NotEmpty();
			RuleFor(x => x.LastName).NotEmpty();

			RuleFor(x => x.NPI).Must(BeUnique);
		}

		private bool BeUnique(string npi)
		{
			return _session.Query<Provider>().Any(x => x.NPI == npi) == false;
		}
	}

	public class CreateProviderHandler : IRequestHandler<CreateProviderCommand, CreateProviderResponse>
	{
		private readonly IDocumentSession _session;

		public CreateProviderHandler(IDocumentSession session)
		{
			_session = session;
		}

		public CreateProviderResponse Handle(CreateProviderCommand message)
		{
			var provider = new Provider
			{
				NPI = message.NPI,
				FirstName = message.FirstName,
				LastName = message.LastName,
				Address = new Address
				{
					LineOne = message.AddressLineOne,
					LineTwo = message.AddressLineTwo,
					City = message.City,
					State = new State
					{
						Name = message.State
					},
					Zip = message.Zip
				}
			};

			_session.Store(provider);
			_session.SaveChanges();

			return new CreateProviderResponse {ProviderId = provider.ProviderId};
		}
	}
}