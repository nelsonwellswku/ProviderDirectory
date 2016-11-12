using System;
using System.Text;
using System.Web.Http;
using Octogami.ProviderDirectory.Application.Domain;

namespace Octogami.ProviderDirectory.Web.Controllers.api
{
	public class HealthCheckController : ApiController
	{
		private readonly Func<Provider> _newProvider;

		public HealthCheckController(Func<Provider> newProvider)
		{
			_newProvider = newProvider;
		}

		[Route("api/healthCheck")]
		public IHttpActionResult GetHealthCheck()
		{
			var builder = new StringBuilder("Performing health checks...");
			try
			{
				builder.AppendLine("Testing StructureMap dependency resolution...");
				var provider = _newProvider();
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