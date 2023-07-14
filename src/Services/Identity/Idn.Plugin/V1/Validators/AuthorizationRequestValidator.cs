using FluentValidation;
using Idn.Contracts;

namespace Idn.Plugin.V1.Validators;

public sealed class AuthorizationRequestValidator : AbstractValidator<AuthorizationRequest>
{
    public AuthorizationRequestValidator()
    {
        RuleFor(request => request.Token).NotEmpty();
    }
}