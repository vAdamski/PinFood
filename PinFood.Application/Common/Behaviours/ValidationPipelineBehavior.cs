using FluentValidation;
using MediatR;
using PinFood.Domain.Common;

namespace PinFood.Application.Common.Behaviours;

public class ValidationPipelineBehavior<TRequest, TResponse>
	: IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
	where TResponse : Result
{
	private readonly IEnumerable<IValidator<TRequest>> _validators;

	public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) =>
		_validators = validators;

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		if (!_validators.Any())
		{
			return await next();
		}

		List<FluentValidation.Results.ValidationResult> results = new();
        
		foreach (var validator in _validators)
		{
			var data = await validator.ValidateAsync(request, cancellationToken);
            
			results.Add(data);
		}

		Error[] errors = results
			.SelectMany(validationResult => validationResult.Errors)
			.Where(validationFailure => validationFailure is not null)
			.Select(failure => new Error(
				failure.PropertyName,
				failure.ErrorMessage))
			.Distinct()
			.ToArray();

		if (errors.Any())
		{
			return CreateValidationResult<TResponse>(errors);
		}

		return await next();
	}

	private static TResult CreateValidationResult<TResult>(Error[] errors)
		where TResult : Result
	{
		if (typeof(TResult) == typeof(Result))
		{
			return (ValidationResult.WithErrors(errors) as TResult)!;
		}

		object validationResult = typeof(ValidationResult<>)
			.GetGenericTypeDefinition()
			.MakeGenericType(typeof(TResult).GenericTypeArguments[0])
			.GetMethod(nameof(ValidationResult.WithErrors))!
			.Invoke(null, new object?[] { errors })!;

		return (TResult)validationResult;
	}
}