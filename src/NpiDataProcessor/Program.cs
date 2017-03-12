using Octogami.ProviderDirectory.Application;
using StructureMap;

namespace Octogami.ProviderDirectory.NpiDataProcessor
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			using (var container = new Container())
			{
				container.Configure(x =>
				{
					x.AddRegistry<ApplicationRegistry>();
					x.AddRegistry<NpiDataProcessorRegistry>();
				});

				var processor = container.GetInstance<NpiProcessorRoot>();
				processor.Process();
			}
		}
	}
}