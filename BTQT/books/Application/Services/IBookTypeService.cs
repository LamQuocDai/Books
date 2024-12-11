using Application.DTOs;

namespace Application.Services;

public interface IBookTypeService
{
    Task<BookTypeDto> GetBookTypeByIdAsync(int id);
    
    Task<IEnumerable<BookTypeDto>> GetAllBookTypesAsync();
    
    Task<BookTypeDto> CreateBookTypeAsync(BookTypeDto bookTypeDto, CancellationToken cancellationToken);
    
    Task<BookTypeDto> UpdateBookTypeAsync(BookTypeDto bookTypeDto, CancellationToken cancellationToken);
    
    Task<BookTypeDto> DeleteBookTypeAsync(int id, CancellationToken cancellationToken);
}