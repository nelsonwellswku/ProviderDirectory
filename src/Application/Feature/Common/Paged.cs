using System.Collections.Generic;

namespace Octogami.ProviderDirectory.Application.Feature.Common
{
	public class Paged<T> : IPaged<T>
	{
		public Paged(IEnumerable<T> items, long totalItems, int currentPage, int currentRecordsPerPage)
		{
			Items = items;
			TotalItems = totalItems;
			CurrentPage = currentPage;
			CurrentRecordsPerPage = currentRecordsPerPage;
		}

		public virtual IEnumerable<T> Items { get; }
		public virtual long TotalItems { get; }
		public virtual int CurrentPage { get; }
		public virtual int CurrentRecordsPerPage { get; }
	}
}