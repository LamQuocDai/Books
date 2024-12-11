using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Password = user.Password
        };
    }
    
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        
        return users.Select(user => new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Password = user.Password
        });
    }
    
    public async Task<UserDto> CreateUserAsync(UserDto userDto, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Name = userDto.Name,
            Email = userDto.Email,
            Phone = userDto.Phone,
            Password = userDto.Password
        };
        
        await _userRepository.CreateUserAsync(user);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Password = user.Password
        };
    }
    
    public async Task<UserDto> UpdateUserAsync(int id, UserDto userDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        
        user.Name = userDto.Name;
        user.Email = userDto.Email;
        user.Phone = userDto.Phone;
        user.Password = userDto.Password;
        
        await _userRepository.UpdateUserAsync(user);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Password = user.Password
        };
    }
    
    public async Task<UserDto> DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.DeleteUserAsync(id);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Password = user.Password
        };
    }
}