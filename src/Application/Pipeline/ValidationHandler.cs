using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using MediatR;

namespace Octogami.ProviderDirectory.Application.Pipeline
{
	public class ValidationHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;
		private readonly IRequestHandler<TRequest, TResponse> _innerHandler;

		public ValidationHandler(IEnumerable<IValidator<TRequest>> validator, IRequestHandler<TRequest, TResponse> innerHandler)
		{
			_validators = validator;
			_innerHandler = innerHandler;
		}

		public TResponse Handle(TRequest message)
		{
			var failures = _validators
				.Select(x => x.Validate(new ValidationContext<TRequest>(message)))
				.SelectMany(x => x.Errors)
				.Where(x => x != null)
				.ToList();

			if(failures.Any())
			{
				throw new ValidationException(failures);
			}

			return _innerHandler.Handle(message);
		}
	}
}