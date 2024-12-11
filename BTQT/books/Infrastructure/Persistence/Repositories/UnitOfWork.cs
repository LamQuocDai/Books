using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public IUserRepository UserRepository { get; private set; }
    public IBookTypeRepository BookTypeRepository { get; private set; }
    public IAuthorRepository AuthorRepository { get; private set; }
    public IBookRepository BookRepository { get; private set; }
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        UserRepository = new UserRepository(_context);
        BookTypeRepository = new BookTypeRepository(_context);
        AuthorRepository = new AuthorRepository(_context);
        BookRepository = new BookRepository(_context);
    }

    public IUserRepository _userRepository { get; }
    public IBookTypeRepository _bookTypeRepository { get; }
    public IAuthorRepository _authorRepository { get; }
    public IBookRepository _bookRepository { get; }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public async Task<int> CompleteAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
    

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}