using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Octogami.ProviderDirectory.Application.Pipeline
{
	public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>>  _validators;

		public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			_validators = validators;
		}

		public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
		{
			var failures = _validators
				.Select(x => x.Validate(new ValidationContext<TRequest>(request)))
				.SelectMany(x => x.Errors)
				.Where(x => x != null)
				.ToList();

			if (failures.Any())
			{
				throw new ValidationException(failures);
			}

			return next();
		}
	}
}
