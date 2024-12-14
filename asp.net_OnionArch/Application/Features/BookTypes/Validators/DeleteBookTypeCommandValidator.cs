using Application.Features.BookTypes.Commands;
using Domain.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Features.BookTypes.Validators;

public class DeleteBookTypeCommandValidator : AbstractValidator<DeleteBookTypeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteBookTypeCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.")
            .MustAsync(BookTypeExists).WithMessage("Book Type not found.");
    }
    
    private async Task<bool> BookTypeExists(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.BookTypeRepository.GetBookTypeByIdAsync(id, cancellationToken) != null;
    }
}