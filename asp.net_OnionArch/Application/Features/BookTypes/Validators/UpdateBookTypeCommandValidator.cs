using Application.Features.BookTypes.Commands;
using Domain.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Features.BookTypes.Validators;

public class UpdateBookTypeCommandValidator : AbstractValidator<UpdateBookTypeCommand>
{
    private readonly IBookTypeRepository _bookTypeRepository;
    
    public UpdateBookTypeCommandValidator(IBookTypeRepository bookTypeRepository)
    {
        _bookTypeRepository = bookTypeRepository;

        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.")
            .MustAsync(BookTypeExists).WithMessage("Book Type not found.");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{Name is required.");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");
    }
    
    private async Task<bool> BookTypeExists(int id, CancellationToken cancellationToken)
    {
        return await _bookTypeRepository.GetBookTypeByIdAsync(id, cancellationToken) != null;
    }
}