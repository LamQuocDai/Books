using Application.Features.Authors.Commands;
using FluentValidation;

namespace Application.Features.Authors.Validators;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
    }    
}