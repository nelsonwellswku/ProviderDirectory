using Octogami.ProviderDirectory.Application;
using StructureMap;

namespace Octogami.ProviderDirectory.Tests.Integration.TestSupport
{
	public class TestContainer
	{
		public IContainer GetContainer()
		{
			var container = new Container(new ApplicationRegistry());
			return container;
		}
	}
}