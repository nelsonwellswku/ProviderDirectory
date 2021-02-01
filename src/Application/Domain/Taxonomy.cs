using System;
using Marten.Schema;

namespace Octogami.ProviderDirectory.Application.Domain
{
	public class Taxonomy
	{
		[Identity]
		public Guid TaxonomyId { get; set; }

		public string TaxonomyCode { get; set; }

		public string Grouping { get; set; }

		public string Classification { get; set; }

		public string Specialization { get; set; }

		public string Definition { get; set; }

		public string Notes { get; set; }
	}
}