using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class BookTypeRepository : IBookTypeRepository
{
    private readonly ApplicationDbContext _context;
    
    public BookTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<BookType> GetBookTypeByIdAsync(int id)
    {
        return await _context.BookTypes.FindAsync(id)  ?? throw new Exception("BookType not found");
    }
    
    public async Task<IEnumerable<BookType>> GetAllBookTypesAsync()
    {
        return await _context.BookTypes.ToListAsync();
    }
    
    public async Task<BookType> CreateBookTypeAsync(BookType bookType)
    {
        await _context.BookTypes.AddAsync(bookType);
        return bookType;
    }
    
    public async Task<BookType> UpdateBookTypeAsync(BookType bookType)
    {
        _context.BookTypes.Update(bookType);
        return bookType;
    }
    
    public async Task<BookType> DeleteBookTypeAsync(int id)
    {
        var bookType = await _context.BookTypes.FindAsync(id) ?? throw new Exception("BookType not found");
        _context.BookTypes.Remove(bookType);
        return bookType;
    }
}