using Application.DTOs;

namespace Application.Services;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int id);
    
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    
    Task<UserDto> CreateUserAsync(UserDto userDto, CancellationToken cancellationToken);
    
    Task<UserDto> UpdateUserAsync(int id, UserDto userDto, CancellationToken cancellationToken);
    
    Task<UserDto> DeleteUserAsync(int id, CancellationToken cancellationToken);
}