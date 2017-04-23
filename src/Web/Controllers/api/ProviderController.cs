using System;
using System.Threading;
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
		public async Task<IHttpActionResult> PostProvider(CreateProviderCommand command, CancellationToken cancellationToken)
		{
			var response = await _mediator.Send(command, cancellationToken);
			return Ok(response);
		}

		[Route("api/Providers/{providerId}")]
		public async Task<IHttpActionResult> GetProvider(Guid providerId, CancellationToken cancellationToken)
		{
			var response = await _mediator.Send(new GetProviderQuery {ProviderId = providerId}, cancellationToken);
			return Ok(response);
		}

		[Route("api/Providers")]
		public async Task<IHttpActionResult> GetProviders([FromUri]ListProvidersQuery query, CancellationToken cancellationToken)
		{
			var response = await _mediator.Send(query ?? new ListProvidersQuery(), cancellationToken);
			return Ok(response);
		}
	}
}