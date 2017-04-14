using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octogami.ProviderDirectory.Application.Feature.Common
{
	public class TaxonomyResponse
	{
		public Guid TaxonomyId { get; set; }

		public string TaxonomyCode { get; set; }

		public string Grouping { get; set; }

		public string Classification { get; set; }

		public string Specialization { get; set; }

		public string Definition { get; set; }

		public string Notes { get; set; }
	}
}
