using Application.Features.Books.Commands;
using Domain.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Features.Books.Validators;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateBookCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required.");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("Author Id is required.")
            .MustAsync(AuthorExists).WithMessage("Author does not exist.");

        RuleFor(x => x.BookTypeId)
            .NotEmpty().WithMessage("Book Type Id is required.")
            .MustAsync(BookTypeExists).WithMessage("Book Type does not exist.");
        
        RuleFor(x => x.ReleaseDate)
            .NotEmpty().WithMessage("Release Date is required.");
    }
    
    private async Task<bool> AuthorExists(int authorId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(authorId, cancellationToken) != null;
    }
    
    private async Task<bool> BookTypeExists(int bookTypeId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.BookTypeRepository.GetBookTypeByIdAsync(bookTypeId, cancellationToken) != null;
    }
}