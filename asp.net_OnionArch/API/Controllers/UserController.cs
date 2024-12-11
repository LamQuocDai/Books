using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;
    
    public UserController(IMediator mediator, ILogger<UserController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetUsers()
    {
        try
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting users");
            return StatusCode(500,"Error occurred while getting users");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetUserById(int id)
    {
        try
        {
            var user = await _mediator.Send(new GetUserByIdQuery {Id = id});
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting user by id");
            return StatusCode(500,"Error occurred while getting user by id");
        }
    }
    
    [HttpGet("email/{email}")]
    public async Task<ActionResult> GetUserByEmail(string email)
    {
        try
        {
            var user = await _mediator.Send(new GetUserByEmailQuery {Email = email});
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting user by email");
            return StatusCode(500,"Error occurred while getting user by email");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        try
        {
            var user = await _mediator.Send(command);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while creating user");
            return StatusCode(500,"Error occurred while creating user");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
    {
        try
        {
            command.Id = id;
            var user = await _mediator.Send(command);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while updating user");
            return StatusCode(500,"Error occurred while updating user");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var user = await _mediator.Send(new DeleteUserCommand {Id = id});
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while deleting user");
            return StatusCode(500,"Error occurred while deleting user");
        }
    }
}