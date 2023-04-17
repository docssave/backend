using FluentValidation;

namespace Idn.Contracts.Validators;

public sealed class AuthorizationRequestValidator : AbstractValidator<AuthorizationRequest>
{
    public AuthorizationRequestValidator()
    {
        RuleFor(request => request.Token).NotEmpty();
    }
}