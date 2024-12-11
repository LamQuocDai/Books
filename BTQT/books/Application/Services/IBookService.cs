using Application.DTOs;

namespace Application.Services;

public interface IBookService
{
    Task<BookDto> GetBookByIdAsync(int id);
    
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    
    Task<BookDto> CreateBookAsync(BookDto bookDto, CancellationToken cancellationToken);
    
    Task<BookDto> UpdateBookAsync(BookDto bookDto, CancellationToken cancellationToken);
    
    Task<BookDto> DeleteBookAsync(int id, CancellationToken cancellationToken);
}