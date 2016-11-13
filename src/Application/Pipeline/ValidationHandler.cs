using System.Linq;
using FluentValidation;
using MediatR;

namespace Octogami.ProviderDirectory.Application.Pipeline
{
	public class ValidationHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly IValidator<TRequest> _validator;
		private readonly IRequestHandler<TRequest, TResponse> _innerHandler;

		public ValidationHandler(IValidator<TRequest> validator, IRequestHandler<TRequest, TResponse> innerHandler)
		{
			_validator = validator;
			_innerHandler = innerHandler;
		}

		public TResponse Handle(TRequest message)
		{
			var failures = _validator.Validate(message).Errors.Where(x => x != null).ToList();
			if(failures.Any())
			{
				throw new ValidationException(failures);
			}

			return _innerHandler.Handle(message);
		}
	}
}