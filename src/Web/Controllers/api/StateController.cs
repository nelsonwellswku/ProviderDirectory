using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using Octogami.ProviderDirectory.Application.Feature.GetStates;

namespace Octogami.ProviderDirectory.Web.Controllers.api
{
	public class StateController : ApiController
	{
		private readonly IMediator _mediator;

		public StateController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[Route("api/states")]

		public Task<IEnumerable<GetStatesResponse>> GetStates()
		{
			var result = _mediator.Send(new GetStatesRequest());
			return result;
		}
	}
}