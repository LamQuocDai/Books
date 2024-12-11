using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id) ?? throw new Exception("User not found");
    }
    
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
    
    public async Task<User> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        return user;
    }
    
    public async Task<User> UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        return user;
    }
    
    public async Task<User> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id) ?? throw new Exception("User not found");
        _context.Users.Remove(user);
        return user;
    }
}