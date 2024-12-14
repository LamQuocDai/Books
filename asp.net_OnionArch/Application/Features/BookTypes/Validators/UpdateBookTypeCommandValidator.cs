using Application.Features.BookTypes.Commands;
using Domain.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Features.BookTypes.Validators;

public class UpdateBookTypeCommandValidator : AbstractValidator<UpdateBookTypeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateBookTypeCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.")
            .MustAsync(BookTypeExists).WithMessage("Book Type not found.");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{Name is required.");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");
    }
    
    private async Task<bool> BookTypeExists(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.BookTypeRepository.GetBookTypeByIdAsync(id, cancellationToken) != null;
    }
}