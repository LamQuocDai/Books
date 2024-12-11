using Application.DTOs;

namespace Application.Services;

public interface IAuthorService
{
    Task<AuthorDto> GetAuthorByIdAsync(int id);
    
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
    
    Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto, CancellationToken cancellationToken);
    
    Task<AuthorDto> UpdateAuthorAsync(AuthorDto authorDto, CancellationToken cancellationToken);
    
    Task<AuthorDto> DeleteAuthorAsync(int id, CancellationToken cancellationToken);
}