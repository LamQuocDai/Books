using Domain.Entities;
using Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Infrastructure.Repositories;

public class BookTypeRepository : IBookTypeRepository
{
    
    private readonly ApplicationDbContext _context;
    
    public BookTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IQueryable<BookType> GetBookTypes()
    {
        return _context.BookTypes.AsQueryable();
    }
    
    public async Task<IEnumerable<BookType>> GetBookTypesAsync(CancellationToken cancellationToken)
    {
        return await _context.BookTypes.ToListAsync(cancellationToken);
    }
    
    public async Task<BookType?> GetBookTypeByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.BookTypes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    
    public async Task<BookType?> InsertBookTypeAsync(BookType bookType, CancellationToken cancellationToken)
    {
        await _context.BookTypes.AddAsync(bookType, cancellationToken);
        return bookType;
    }
    
    public async Task<BookType?> DeleteBookTypeByIdAsync(int id, CancellationToken cancellationToken)
    {
        var bookType = await _context.BookTypes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (bookType == null) return null;
        
        _context.BookTypes.Remove(bookType);
        return bookType;
    }
    

}