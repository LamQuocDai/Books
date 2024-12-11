using Domain.Entities;

namespace Domain.Interfaces.IRepositories;
public interface IUserRepository
{
    // This method is return IQueryable of User
    IQueryable<User> GetUsers();
    
    // This method is async and returns a Task of IEnumerable (List) of User
    Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken);
    
    // This method is async and returns a Task of User
    Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    
    // This method is async and returns a Task of User (by email)
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    
    // This method is insert new User
    Task<User?> InsertUserAsync(User user, CancellationToken cancellationToken);
    
    // This method is delete User by Id
    Task<User?> DeleteUserByIdAsync(int id, CancellationToken cancellationToken);
    
    
}