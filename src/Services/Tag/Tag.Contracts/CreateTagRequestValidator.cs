using FluentValidation;

namespace TagContracts;

public sealed class CreateTagRequestValidator : AbstractValidator<CreateTagRequest>
{
    public CreateTagRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
    }
}