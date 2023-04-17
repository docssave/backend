using FluentValidation;

namespace WebApi.Filters;

internal sealed class ValidatorFilter<TRequest> : IEndpointFilter where TRequest : class
{
    private readonly IValidator<TRequest> _validator;

    public ValidatorFilter(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var argument = context.Arguments.SingleOrDefault(argument => argument.GetType() == typeof(TRequest));

        if (argument is not TRequest request)
        {
            throw new ArgumentException($"Could not find request with the following type `{typeof(TRequest)}`");
        }

        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        }

        return await next(context);
    }
}