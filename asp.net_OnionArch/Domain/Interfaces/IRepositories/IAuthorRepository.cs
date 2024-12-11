using Domain.Entities;

namespace Domain.Interfaces.IRepositories;

public interface IAuthorRepository
{
    // This method is return IQueryable of Author
    IQueryable<Author> GetAuthors();
    
    // This method is async and returns a Task of IEnumerable (List) of Author
    Task<IEnumerable<Author>> GetAuthorsAsync(CancellationToken cancellationToken);
    
    // This method is async and returns a Task of Author (by id)
    Task<Author?> GetAuthorByIdAsync(int id, CancellationToken cancellationToken);
    
    // This method is insert new Author
    Task<Author?> InsertAuthorAsync(Author author, CancellationToken cancellationToken);
    
    // This method is delete Author by Id
    Task<Author?> DeleteAuthorByIdAsync(int id, CancellationToken cancellationToken);
    
}