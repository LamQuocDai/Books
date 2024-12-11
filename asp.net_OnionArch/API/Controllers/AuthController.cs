using Application.Exceptions;
using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;
    
    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
    {
        try
        {
            var user = await _mediator.Send(loginQuery);
            return Ok(user);
        }
        catch (NotFoundException e)
        {
            _logger.LogError(e, e.Message);
            return NotFound(e.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            _logger.LogError(e, e.Message);
            return Unauthorized(e.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while logging in");
            return StatusCode(500,"Error occurred while logging in");
        }
    }
}