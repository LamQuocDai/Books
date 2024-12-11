using Application.Features.Books.Commands;
using Domain.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Features.Books.Validators;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand> 
{
    private readonly IBookTypeRepository _bookTypeRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;
    
    public UpdateBookCommandValidator(IBookTypeRepository bookTypeRepository, IAuthorRepository authorRepository, IBookRepository bookRepository)
    {
        _bookTypeRepository = bookTypeRepository;
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .MustAsync(BookExists).WithMessage("Book not found.");

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
    
    private async Task<bool> BookExists(int id, CancellationToken cancellationToken)
    {
        return await _bookRepository.GetBookByIdAsync(id, cancellationToken) != null;
    }
    
    private async Task<bool> AuthorExists(int authorId, CancellationToken cancellationToken)
    {
        return await _authorRepository.GetAuthorByIdAsync(authorId, cancellationToken) != null;
    }
    
    private async Task<bool> BookTypeExists(int bookTypeId, CancellationToken cancellationToken)
    {
        return await _bookTypeRepository.GetBookTypeByIdAsync(bookTypeId, cancellationToken) != null;
    }
}