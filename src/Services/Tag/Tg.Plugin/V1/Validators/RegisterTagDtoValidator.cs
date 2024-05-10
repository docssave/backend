using FluentValidation;
using Tg.Plugin.V1.Dtos;

namespace Tg.Plugin.V1.Validators;

public sealed class RegisterTagDtoValidator : AbstractValidator<RegisterTagDto>
{
    public RegisterTagDtoValidator()
    {
        RuleFor(request => request.Value).NotEmpty();
    }
}