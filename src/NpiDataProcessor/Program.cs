using System.Threading.Tasks;
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

				ProcessAsync(container).Wait();
			}
		}

		private static async Task ProcessAsync(IContainer container)
		{
			var processor = container.GetInstance<NpiProcessorRoot>();
			await processor.Process();
		}
	}
}