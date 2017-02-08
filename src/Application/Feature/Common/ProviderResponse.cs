using System;

namespace Octogami.ProviderDirectory.Application.Feature.Common
{
	public class ProviderResponse
	{
		public Guid ProviderId { get; set; }

		public string NPI { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public Address MailingAddress { get; set; }

		public Address PracticeAddress { get; set; }
	}

	public class Address
	{
		public string StreetOne { get; set; }

		public string StreetTwo { get; set; }

		public string City { get; set; }

		public State State { get; set; }

		public string Zip { get; set; }
	}

	public class State
	{
		public string Name { get; set; }

		public string Abbreviation { get; set; }
	}
}