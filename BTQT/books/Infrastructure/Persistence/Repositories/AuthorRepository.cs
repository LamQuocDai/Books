using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _context;
    
    public AuthorRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Author> GetAuthorByIdAsync(int id)
    {
        return await _context.Authors.FindAsync(id) ?? throw new Exception("Author not found");
    }
    
    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await _context.Authors.ToListAsync();
    }
    
    public async Task<Author> CreateAuthorAsync(Author author)
    {
        await _context.Authors.AddAsync(author);
        return author;
    }
    
    public async Task<Author> UpdateAuthorAsync(Author author)
    {
        _context.Authors.Update(author);
        return author;
    }
    
    public async Task<Author> DeleteAuthorAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id) ?? throw new Exception("Author not found");
        _context.Authors.Remove(author);
        return author;
    }
}