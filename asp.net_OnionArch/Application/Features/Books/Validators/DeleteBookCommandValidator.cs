using Application.DTOs;
using Application.Features.Books.Commands;
using Domain.Interfaces.IRepositories;
using FluentValidation;
using MediatR;

namespace Application.Features.Books.Validators;

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
   private readonly IBookRepository _bookRepository;
   
   public DeleteBookCommandValidator(IBookRepository bookRepository)
   {
       _bookRepository = bookRepository;
       
       RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Id is required.")
           .MustAsync(BookExists).WithMessage("Book not found.");
   }
   
    private async Task<bool> BookExists(int id, CancellationToken cancellationToken)
    {
         return await _bookRepository.GetBookByIdAsync(id, cancellationToken) != null;
    }
}