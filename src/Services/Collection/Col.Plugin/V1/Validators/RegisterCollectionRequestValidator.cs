using Col.Plugin.V1.Dtos;
using FluentValidation;

namespace Col.Plugin.V1.Validators;

internal sealed class RegisterCollectionRequestValidator : AbstractValidator<RegisterCollectionDto>
{
    public RegisterCollectionRequestValidator()
    {
        RuleFor(request => request.Icon).NotEmpty();
        RuleFor(request => request.Name).NotEmpty();
    }
}