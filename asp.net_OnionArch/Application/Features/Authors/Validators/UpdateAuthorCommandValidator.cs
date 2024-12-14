using Application.Features.Authors.Commands;
using Domain.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Features.Authors.Validators;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateAuthorCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .MustAsync(AuthorExists).WithMessage("Author not found.");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
    }
    
    private async Task<bool> AuthorExists(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(id, cancellationToken) != null;
    }
}