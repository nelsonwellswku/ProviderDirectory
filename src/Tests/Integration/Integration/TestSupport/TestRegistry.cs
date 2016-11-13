using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Octogami.ProviderDirectory.Application.Pipeline;
using Octogami.ProviderDirectory.Tests.Integration.Pipeline;
using StructureMap;

namespace Octogami.ProviderDirectory.Tests.Integration.TestSupport
{
	public class TestRegistry : Registry
	{
		public TestRegistry()
		{
			For<IRequestHandler<TestCommand, TestResponse>>().Use<TestHandler>();
			For<IValidator<TestCommand>>().Use<TestValidator>();
		}
	}
}
