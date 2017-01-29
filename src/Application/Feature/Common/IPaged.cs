using System.Collections.Generic;

namespace Octogami.ProviderDirectory.Application.Feature.Common
{
	public interface IPaged<out T>
	{
		IEnumerable<T> Items { get; }
		long TotalItems { get; }
		int CurrentPage { get; }
		int CurrentRecordsPerPage { get; }
	}
}