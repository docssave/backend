using FluentValidation;
using Tg.Contracts.V1;

namespace Tg.Plugin.V1.Validators;

public sealed class CreateTagRequestValidator : AbstractValidator<RegisterTagRequest>
{
    public CreateTagRequestValidator()
    {
        RuleFor(request => request.Value).NotEmpty();
    }
}