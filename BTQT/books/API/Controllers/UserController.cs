using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace books.Controllers;

[ApiController]
[Route("api/users")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserDto userDto, CancellationToken cancellationToken)
    {
        var user = await _userService.CreateUserAsync(userDto, cancellationToken);
        return Ok(user);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserDto userDto)
    {
        var user = await _userService.UpdateUserAsync(id, userDto, CancellationToken.None);
        return Ok(user);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        var user = await _userService.DeleteUserAsync(id, CancellationToken.None);
        return Ok(user);
    }
}