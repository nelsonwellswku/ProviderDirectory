using System;

namespace Octogami.ProviderDirectory.Application.Feature.Common
{
	public class ProviderResponse
	{
		public Guid ProviderId { get; set; }
		public string NPI { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}