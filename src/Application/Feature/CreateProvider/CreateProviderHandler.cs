﻿using System;
using System.Linq;
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

		public Address MailingAddress { get; set; }

		public Address PracticeAddress { get; set; }
	}

	public class Address
	{
		public string LineOne { get; set; }

		public string LineTwo { get; set; }

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
				EntityType = message.EntityType == null ? EntityType.Unknown : (EntityType) Enum.Parse(typeof(EntityType), message.EntityType, true),
				EnumerationDate = message.EnumerationDate == null ? DateTime.Now : DateTime.Parse(message.EnumerationDate),
				FirstName = message.FirstName,
				MiddleName = message.MiddleName,
				LastName = message.LastName,
				Gender = message.Gender == null ? Gender.Unknown : (Gender) Enum.Parse(typeof(Gender), message.Gender, true),
				MailingAddress = new Domain.Address
				{
					LineOne = message.MailingAddress?.LineOne,
					LineTwo = message.MailingAddress?.LineTwo,
					City = message.MailingAddress?.City,
					State = new State
					{
						Name = message.MailingAddress?.State
					},
					Zip = message.MailingAddress?.Zip
				},
				PracticeAddress = new Domain.Address
				{
					LineOne = message.PracticeAddress?.LineOne,
					LineTwo = message.PracticeAddress?.LineTwo,
					City = message.PracticeAddress?.City,
					State = new State
					{
						Name = message.PracticeAddress?.State
					},
					Zip = message.PracticeAddress?.Zip
				}
			};

			_session.Store(provider);
			_session.SaveChanges();

			return new CreateProviderResponse {ProviderId = provider.ProviderId};
		}
	}
}