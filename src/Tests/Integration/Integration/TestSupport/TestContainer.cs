using Octogami.ProviderDirectory.Application;
using StructureMap;

namespace Octogami.ProviderDirectory.Tests.Integration.TestSupport
{
	public static class TestContainerFactory
	{
		public static IContainer New()
		{
			var container = new Container(new ApplicationRegistry());
			return container;
		}
	}
}