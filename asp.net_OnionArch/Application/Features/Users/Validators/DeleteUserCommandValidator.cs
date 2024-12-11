using Application.Features.Users.Commands;
using Domain.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Features.Users.Validators;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    
    public DeleteUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .MustAsync(UserExists).WithMessage("User not found.");
    }
    
    private async Task<bool> UserExists(int id, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserByIdAsync(id, cancellationToken) != null;
    }
}