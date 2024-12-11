using Domain.Entities;
using Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _context;
    
    public AuthorRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IQueryable<Author> GetAuthors()
    {
        return _context.Authors.AsQueryable();
    }
    
    public async Task<IEnumerable<Author>> GetAuthorsAsync(CancellationToken cancellationToken)
    {
        return await _context.Authors.ToListAsync(cancellationToken);
    }
    
    public async Task<Author?> GetAuthorByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Authors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    
    public async Task<Author?> InsertAuthorAsync(Author author, CancellationToken cancellationToken)
    {
        await _context.Authors.AddAsync(author, cancellationToken);
        return author;
    }
    
    public async Task<Author?> DeleteAuthorByIdAsync(int id, CancellationToken cancellationToken)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (author == null) return null;
        
        _context.Authors.Remove(author);
        return author;
    }
    
}