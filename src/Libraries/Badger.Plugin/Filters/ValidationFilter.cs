﻿using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Badger.Plugin.Filters;

public sealed class ValidationFilter<TRequest> : IEndpointFilter where TRequest : class
{
    private readonly IValidator<TRequest> _validator;

    public ValidationFilter(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var argument = context.Arguments.SingleOrDefault(argument => argument?.GetType() == typeof(TRequest));

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