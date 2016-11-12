using System;
using MediatR;

namespace Octogami.ProviderDirectory.Application.Feature.AddProvider
{
	public class AddProviderCommand : IRequest<AddProviderResponse>
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

	public class AddProviderResponse
	{
		public Guid ProviderId { get; set; }
	}

	public class AddProviderHandler : IRequestHandler<AddProviderCommand, AddProviderResponse>
	{
		public AddProviderResponse Handle(AddProviderCommand message)
		{
			return new AddProviderResponse {ProviderId = Guid.NewGuid()};
		}
	}
}