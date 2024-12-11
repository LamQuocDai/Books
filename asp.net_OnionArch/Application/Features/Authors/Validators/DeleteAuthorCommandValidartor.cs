using Application.Features.Authors.Commands;
using Domain.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Features.Authors.Validators;

public class DeleteAuthorCommandValidartor : AbstractValidator<DeleteAuthorCommand>
{
    private readonly IAuthorRepository _authorRepository;
    
    public DeleteAuthorCommandValidartor(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
        
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .MustAsync(AuthorExists).WithMessage("Author not found.");
    }
    
    private async Task<bool> AuthorExists(int id, CancellationToken cancellationToken)
    {
        return await _authorRepository.GetAuthorByIdAsync(id, cancellationToken) != null;
    }
}