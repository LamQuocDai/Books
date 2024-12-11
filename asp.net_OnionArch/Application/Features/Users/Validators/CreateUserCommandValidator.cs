using Application.Features.Users.Commands;
using FluentValidation;

namespace Application.Features.Users.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.");
        
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required.");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}