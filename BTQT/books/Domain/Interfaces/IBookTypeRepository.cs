using Domain.Entities;

namespace Domain.Interfaces;

public interface IBookTypeRepository
{
    Task<BookType> GetBookTypeByIdAsync(int id);
    Task<IEnumerable<BookType>> GetAllBookTypesAsync();
    Task<BookType> CreateBookTypeAsync(BookType bookType);
    Task<BookType> UpdateBookTypeAsync(BookType bookType);
    Task<BookType> DeleteBookTypeAsync(int id);
}