using FluentValidation;
using Idn.Contracts.V1;

namespace Idn.Plugin.V1.Validators;

internal sealed class AuthorizationRequestValidator : AbstractValidator<AuthorizationRequest>
{
    public AuthorizationRequestValidator()
    {
        RuleFor(request => request.Token).NotEmpty();
    }
}