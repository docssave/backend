using Doc.Plugin.V1.Dtos;
using FluentValidation;

namespace Doc.Plugin.V1.Validators;

internal sealed class RegisterDocumentDtoValidator : AbstractValidator<RegisterDocumentDto>
{
    public RegisterDocumentDtoValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Icon).NotEmpty();
    }
}