using Domain.Entities;

namespace Domain.Interfaces;

public interface IAuthorRepository
{
    Task<Author> GetAuthorByIdAsync(int id);
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<Author> CreateAuthorAsync(Author author);
    Task<Author> UpdateAuthorAsync(Author author);
    Task<Author> DeleteAuthorAsync(int id);
}