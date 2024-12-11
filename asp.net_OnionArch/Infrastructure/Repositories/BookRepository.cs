using Domain.Entities;
using Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;
    
    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IQueryable<Book> GetBooks()
    {
        return _context.Books.AsQueryable();
    }
    
    public async Task<IEnumerable<Book>> GetBooksAsync(CancellationToken cancellationToken)
    {
        return await _context.Books.ToListAsync(cancellationToken);
    }
    
    public async Task<Book?> GetBookByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Books.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    
    public async Task<Book?> InsertBookAsync(Book book, CancellationToken cancellationToken)
    {
        await _context.Books.AddAsync(book, cancellationToken);
        return book;
    }
    
    public async Task<Book?> DeleteBookByIdAsync(int id, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (book == null) return null;
        
        _context.Books.Remove(book);
        return book;
    }
    

}