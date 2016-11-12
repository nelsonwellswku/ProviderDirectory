﻿using System.Web.Http;
using MediatR;
using Octogami.ProviderDirectory.Application.Feature.AddProvider;

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
		public IHttpActionResult PostProvider(AddProviderCommand command)
		{
			var response = _mediator.Send(command);
			return Ok(response);
		}
	}
}