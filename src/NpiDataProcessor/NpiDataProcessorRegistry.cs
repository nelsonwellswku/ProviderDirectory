using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace Octogami.ProviderDirectory.NpiDataProcessor
{
	public class NpiDataProcessorRegistry : Registry
	{
		public NpiDataProcessorRegistry()
		{
			For<ApplicationConfiguration>().Use<ApplicationConfiguration>();
			For<NpiProcessorRoot>().Use<NpiProcessorRoot>();
		}
	}
}
