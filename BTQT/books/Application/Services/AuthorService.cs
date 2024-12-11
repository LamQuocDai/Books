using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Repositories;

namespace Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public AuthorService(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AuthorDto> GetAuthorByIdAsync(int id)
    {
        var author = await _authorRepository.GetAuthorByIdAsync(id);
        
        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
        };
    }
    
    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
    {
        var authors = await _authorRepository.GetAllAuthorsAsync();
        
        return authors.Select(author => new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
        });
    }
    
    public async Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto, CancellationToken cancellationToken)
    {
        var author = new Author()
        {
            Name = authorDto.Name,
        };
        
        await _authorRepository.CreateAuthorAsync(author);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
        };
    }
    
    public async Task<AuthorDto> UpdateAuthorAsync(AuthorDto authorDto, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetAuthorByIdAsync(authorDto.Id);
        author.Name = authorDto.Name;
        
        await _authorRepository.UpdateAuthorAsync(author);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
        };
    }
    
    public async Task<AuthorDto> DeleteAuthorAsync(int id, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.DeleteAuthorAsync(id);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
        };
    }
    
}