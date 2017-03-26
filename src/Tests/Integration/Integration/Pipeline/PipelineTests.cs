using FluentValidation;
using FluentValidation.Results;
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
			var container = TestContainerFactory.New();
			container.Configure(x => x.AddRegistry<TestRegistry>());

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

	public class TestValidator : AbstractValidator<TestCommand>
	{
		public bool WasValidated { get; private set; }

		public override ValidationResult Validate(ValidationContext<TestCommand> validationContext)
		{
			WasValidated = true;
			return base.Validate(validationContext);
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