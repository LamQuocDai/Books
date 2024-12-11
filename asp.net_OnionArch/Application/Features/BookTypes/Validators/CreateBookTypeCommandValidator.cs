using Application.Features.BookTypes.Commands;
using FluentValidation;

namespace Application.Features.BookTypes.Validators;

public class CreateBookTypeCommandValidator : AbstractValidator<CreateBookTypeCommand>
{
    public CreateBookTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");
    }
}