using Application.Features.Authors.Commands;
using Domain.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Features.Authors.Validators;

public class DeleteAuthorCommandValidartor : AbstractValidator<DeleteAuthorCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteAuthorCommandValidartor(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .MustAsync(AuthorExists).WithMessage("Author not found.");
    }
    
    private async Task<bool> AuthorExists(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(id, cancellationToken) != null;
    }
}