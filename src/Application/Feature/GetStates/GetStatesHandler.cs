using System.Collections.Generic;
using System.Linq;
using MediatR;
using Octogami.ProviderDirectory.Application.Domain;

namespace Octogami.ProviderDirectory.Application.Feature.GetStates
{
	public class GetStatesRequest : IRequest<IEnumerable<GetStatesResponse>>
	{
	}

	public class GetStatesResponse
	{
		public string Name { get; set; }
		public string Abbreviation { get; set; }
	}

	public class GetStatesHandler : IRequestHandler<GetStatesRequest, IEnumerable<GetStatesResponse>>
	{
		private readonly IStateService _stateService;

		public GetStatesHandler(IStateService stateService)
		{
			_stateService = stateService;
		}

		public IEnumerable<GetStatesResponse> Handle(GetStatesRequest message)
		{
			return _stateService.States.Select(x => new GetStatesResponse {Name = x.Name, Abbreviation = x.Abbreviation});
		}
	}
}