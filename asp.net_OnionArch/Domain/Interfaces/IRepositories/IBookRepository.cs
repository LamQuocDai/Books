using Domain.Entities;

namespace Domain.Interfaces.IRepositories;

public interface IBookRepository
{
    IQueryable<Book> GetBooks();
    
    // This method is async and returns a Task of IEnumerable (List) of Book
    Task<IEnumerable<Book>> GetBooksAsync(CancellationToken cancellationToken);
    
    // This method is async and returns a Task of Book (by id)
    Task<Book?> GetBookByIdAsync(int id, CancellationToken cancellationToken);
    
    // This method is insert new Book
    Task<Book?> InsertBookAsync(Book book, CancellationToken cancellationToken);
    
    // This method is delete Book by Id
    Task<Book?> DeleteBookByIdAsync(int id, CancellationToken cancellationToken);

}