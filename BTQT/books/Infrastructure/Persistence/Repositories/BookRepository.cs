using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;
    
    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Book> GetBookByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id) ?? throw new Exception("Book not found");
    }
    
    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }
    
    public async Task<Book> CreateBookAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        return book;
    }
    
    public async Task<Book> UpdateBookAsync(Book book)
    {
        _context.Books.Update(book);
        return book;
    }
    
    public async Task<Book> DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id) ?? throw new Exception("Book not found");
        _context.Books.Remove(book);
        return book;
    }
}