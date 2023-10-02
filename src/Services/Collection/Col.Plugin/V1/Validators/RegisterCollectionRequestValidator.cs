using Col.Contracts.V1;
using FluentValidation;

namespace Col.Plugin.V1.Validators;

internal sealed class RegisterCollectionRequestValidator : AbstractValidator<RegisterCollectionRequest>
{
    public RegisterCollectionRequestValidator()
    {
        RuleFor(request => request.Icon).NotEmpty();
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Id).NotEqual(collectionId => CollectionId.Empty);
    }
}