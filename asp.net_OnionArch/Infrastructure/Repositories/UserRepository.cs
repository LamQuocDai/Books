using Domain.Entities;
using Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Infrastructure.Repositories;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IQueryable<User> GetUsers()
    {
        return _context.Users.AsQueryable();
    }
    
    public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken)
    {
        return await _context.Users.ToListAsync(cancellationToken);
    }
    
    public async Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
    
    public async Task<User?> InsertUserAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        return user;
    }
    
    
    public async Task<User?> DeleteUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (user == null) return null;
        
        _context.Users.Remove(user);
        return user;
    }
    
}