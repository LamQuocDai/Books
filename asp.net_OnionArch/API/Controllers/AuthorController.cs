using Application.Features.Authors.Commands;
using Application.Features.Authors.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthorController> _logger;
    
    public AuthorController(IMediator mediator, ILogger<AuthorController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAuthors()
    {
        try
        {
            var authors = await _mediator.Send(new GetAuthorsQuery());
            return Ok(authors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting authors");
            return StatusCode(500,"Error occurred while getting authors");
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetAuthorById(int id)
    {
        try
        {
            var author = await _mediator.Send(new GetAuthorByIdQuery {Id = id});
            return Ok(author);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting author by id");
            return StatusCode(500,"Error occurred while getting author by id");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorCommand command)
    {
        try
        {
            var author = await _mediator.Send(command);
            return Ok(author);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while creating author");
            return StatusCode(500,"Error occurred while creating author");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorCommand command)
    {
        try
        {
            command.Id = id;
            var author = await _mediator.Send(command);
            return Ok(author);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while updating author");
            return StatusCode(500,"Error occurred while updating author");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        try
        {
            var author = await _mediator.Send(new DeleteAuthorCommand {Id = id});
            return Ok(author);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while deleting author");
            return StatusCode(500,"Error occurred while deleting author");
        }
    }
}