using System;
using System.Text;
using System.Web.Http;
using MediatR;
using Octogami.ProviderDirectory.Application.Domain;

namespace Octogami.ProviderDirectory.Web.Controllers.api
{
	public class HealthCheckController : ApiController
	{
		private readonly IMediator _mediator;

		public HealthCheckController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[Route("api/healthCheck")]
		public IHttpActionResult GetHealthCheck()
		{
			var builder = new StringBuilder("Performing health checks...");
			try
			{
				builder.AppendLine("Testing StructureMap dependency resolution...");
				if(_mediator == null) throw new InvalidOperationException("Can't resolve mediator.");
				builder.AppendLine("StructureMap dependency resolution passed.");
			}
			catch(Exception e)
			{
				builder.AppendLine("Health check failed.");
				builder.AppendLine(e.Message);

				return BadRequest(builder.ToString());
			}

			builder.AppendLine("All health checks pass.");
			return Ok(builder.ToString());
		}
	}
}