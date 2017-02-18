using System.Collections.Generic;

namespace Octogami.ProviderDirectory.Application.Domain
{
	public interface IStateService
	{
		bool IsValidState(string input);

		State GetState(string input);

		IEnumerable<State> States { get; }
	}
}