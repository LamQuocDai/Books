using Application.Features.BookTypes.Commands;
using Domain.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Features.BookTypes.Validators;

public class DeleteBookTypeCommandValidator : AbstractValidator<DeleteBookTypeCommand>
{
    private readonly IBookTypeRepository _bookTypeRepository;
    
    public DeleteBookTypeCommandValidator(IBookTypeRepository bookTypeRepository)
    {
        _bookTypeRepository = bookTypeRepository;

        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.")
            .MustAsync(BookTypeExists).WithMessage("Book Type not found.");
    }
    
    private async Task<bool> BookTypeExists(int id, CancellationToken cancellationToken)
    {
        return await _bookTypeRepository.GetBookTypeByIdAsync(id, cancellationToken) != null;
    }
}