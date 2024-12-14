using Application.Features.Users.Commands;
using Domain.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Features.Users.Validators;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteUserCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .MustAsync(UserExists).WithMessage("User not found.");
    }
    
    private async Task<bool> UserExists(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.UserRepository.GetUserByIdAsync(id, cancellationToken) != null;
    }
}