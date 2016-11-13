using FluentValidation;
using MediatR;
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