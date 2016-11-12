using System.Web.Http;
using MediatR;

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
		public IHttpActionResult PostProvider()
		{
			return Ok();
		}
	}
}