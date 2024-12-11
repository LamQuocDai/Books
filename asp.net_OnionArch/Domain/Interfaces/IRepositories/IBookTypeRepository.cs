using Domain.Entities;

namespace Domain.Interfaces.IRepositories;

public interface IBookTypeRepository
{
    // This method is return IQueryable of BookType
    IQueryable<BookType> GetBookTypes();
    
    // This method is async and returns a Task of IEnumerable (List) of BookType
    Task<IEnumerable<BookType>> GetBookTypesAsync(CancellationToken cancellationToken);
    
    // This method is async and returns a Task of BookType (by id)
    Task<BookType?> GetBookTypeByIdAsync(int id, CancellationToken cancellationToken);
    
    // This method is insert new BookType
    Task<BookType?> InsertBookTypeAsync(BookType bookType, CancellationToken cancellationToken);
    
    // This method is delete BookType by Id
    Task<BookType?> DeleteBookTypeByIdAsync(int id, CancellationToken cancellationToken);
    

}