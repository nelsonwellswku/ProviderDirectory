using System;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using Octogami.ProviderDirectory.Application.Feature.CreateProvider;
using Octogami.ProviderDirectory.Application.Feature.GetProvider;
using Octogami.ProviderDirectory.Application.Feature.ListProviders;

namespace Octogami.ProviderDirectory.Web.Controllers.api
{
	public class ProviderController : ApiController
	{
		private readonly IMediator _mediator;

		public ProviderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[Route("api/Providers")]
		public async Task<IHttpActionResult> PostProvider(CreateProviderCommand command)
		{
			var response = await _mediator.Send(command);
			return Ok(response);
		}

		[Route("api/Providers/{providerId}")]
		public async Task<IHttpActionResult> GetProvider(Guid providerId)
		{
			var response = await _mediator.Send(new GetProviderQuery {ProviderId = providerId});
			return Ok(response);
		}

		[Route("api/Providers")]
		public async Task<IHttpActionResult> GetProviders([FromUri]ListProvidersQuery query)
		{
			var response = await _mediator.Send(query ?? new ListProvidersQuery());
			return Ok(response);
		}
	}
}