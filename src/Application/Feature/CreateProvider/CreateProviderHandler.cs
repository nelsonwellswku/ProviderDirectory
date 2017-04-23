using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Marten;
using Marten.Util;
using MediatR;
using Octogami.ProviderDirectory.Application.Domain;

namespace Octogami.ProviderDirectory.Application.Feature.CreateProvider
{
	public class CreateProviderCommand : IRequest<CreateProviderResponse>
	{
		public string NPI { get; set; }

		public string EntityType { get; set; }

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }

		public string Gender { get; set; }

		public string EnumerationDate { get; set; }

		public string PrimaryTaxonomyCode { get; set; }

		public Address MailingAddress { get; set; }

		public Address PracticeAddress { get; set; }
	}

	public class Address
	{
		public string StreetOne { get; set; }

		public string StreetTwo { get; set; }

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

			RuleFor(x => x.EnumerationDate).IsDate();

			RuleFor(x => x.NPI).Must(BeUnique);

			RuleFor(x => x.Gender).Matches("^(male|female|other|unknown)$");
			RuleFor(x => x.EntityType).Matches("^(individual|organization)$");

			RuleFor(x => x.PrimaryTaxonomyCode).Must(BeEmptyOrExist);
		}

		private bool BeUnique(string npi)
		{
			return _session.Query<Provider>().Any(x => x.NPI == npi) == false;
		}

		private bool BeEmptyOrExist(string taxonomyCode)
		{
			return string.IsNullOrEmpty(taxonomyCode) || _session.Query<Taxonomy>().Any(x => x.TaxonomyCode == taxonomyCode);
		}
	}

	public class CreateProviderHandler : ICancellableAsyncRequestHandler<CreateProviderCommand, CreateProviderResponse>
	{
		private readonly IDocumentSession _session;
		private readonly IStateService _stateService;

		public CreateProviderHandler(IDocumentSession session, IStateService stateService)
		{
			_session = session;
			_stateService = stateService;
		}

		public async Task<CreateProviderResponse> Handle(CreateProviderCommand message, CancellationToken cancellationToken)
		{
			var taxonomyId = default(Guid);
			if (string.IsNullOrEmpty(message.PrimaryTaxonomyCode) == false)
			{
				taxonomyId =
					await _session.Query<Taxonomy>()
						.Where(x => x.TaxonomyCode == message.PrimaryTaxonomyCode)
						.Select(x => x.TaxonomyId)
						.SingleAsync(cancellationToken);
			}

			var provider = new Provider
			{
				NPI = message.NPI,
				EntityType = message.EntityType == null ? EntityType.Unknown : (EntityType) Enum.Parse(typeof(EntityType), message.EntityType, true),
				EnumerationDate = message.EnumerationDate == null ? DateTime.Now : DateTime.Parse(message.EnumerationDate),
				FirstName = message.FirstName,
				MiddleName = message.MiddleName,
				LastName = message.LastName,
				Gender = message.Gender == null ? Gender.Unknown : (Gender) Enum.Parse(typeof(Gender), message.Gender, true),
				PrimaryTaxonomyId = taxonomyId,
				MailingAddress = new Domain.Address
				{
					StreetOne = message.MailingAddress?.StreetOne,
					StreetTwo = message.MailingAddress?.StreetTwo,
					City = message.MailingAddress?.City,
					State = _stateService.GetState(message.MailingAddress?.State),
					Zip = message.MailingAddress?.Zip
				},
				PracticeAddress = new Domain.Address
				{
					StreetOne = message.PracticeAddress?.StreetOne,
					StreetTwo = message.PracticeAddress?.StreetTwo,
					City = message.PracticeAddress?.City,
					State = _stateService.GetState(message.PracticeAddress?.State),
					Zip = message.PracticeAddress?.Zip
				}
			};

			_session.Store(provider);
			await _session.SaveChangesAsync(cancellationToken);

			return new CreateProviderResponse {ProviderId = provider.ProviderId};
		}
	}
}