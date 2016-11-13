using MediatR;
using NUnit.Framework;
using Octogami.ProviderDirectory.Application.Pipeline;
using Octogami.ProviderDirectory.Tests.Integration.TestSupport;

namespace Octogami.ProviderDirectory.Tests.Integration.Pipeline
{
	public class PipelineTests
	{
		[Test]
		public void DecoratorsAppliedInCorrectOrder()
		{
			// Call order should be
			// Validator -> Handler

			// Arrange
			var container = new TestContainer().GetContainer();
			container.Configure(x => { x.AddRegistry<TestRegistry>(); });

			// Act
			var handler = container.GetInstance<IRequestHandler<TestCommand, TestResponse>>();
			var response = handler.Handle(new TestCommand());

			// Assert
			Assert.IsInstanceOf<ValidationHandler<TestCommand, TestResponse>>(handler);
			Assert.IsNotNull(response);
		}
	}

	public class TestCommand : IRequest<TestResponse>
	{
	}

	public class TestResponse
	{
	}

	public class TestValidator : IValidator<TestCommand>
	{
		public bool WasValidated { get; private set; }

		public void Validate(TestCommand obj)
		{
			WasValidated = true;
		}
	}

	public class TestHandler : IRequestHandler<TestCommand, TestResponse>
	{
		public TestResponse Handle(TestCommand message)
		{
			return new TestResponse();
		}
	}
}