using System.Web.Http;
using MediatR;
using Octogami.ProviderDirectory.Application.Feature.Common;
using Octogami.ProviderDirectory.Application.Feature.GetTaxonomies;

namespace Octogami.ProviderDirectory.Web.Controllers.api
{
	public class TaxonomyController : ApiController
	{
		private readonly IMediator _mediator;

		public TaxonomyController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[Route("api/taxonomy")]
		public IPaged<TaxonomyResponse> GetTaxonomies([FromUri]GetTaxonomiesQuery query)
		{
			return _mediator.Send(query ?? new GetTaxonomiesQuery());
		}
	}
}