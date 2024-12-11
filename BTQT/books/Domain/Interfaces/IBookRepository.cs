using Domain.Entities;

namespace Domain.Interfaces;

public interface IBookRepository
{
    Task<Book> GetBookByIdAsync(int id);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> CreateBookAsync(Book book);
    Task<Book> UpdateBookAsync(Book book);
    Task<Book> DeleteBookAsync(int id);
}