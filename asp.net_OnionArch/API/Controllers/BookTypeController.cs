using Application.Features.BookTypes.Commands;
using Application.Features.BookTypes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/booktypes")]
public class BookTypeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthorController> _logger;
    
    public BookTypeController(IMediator mediator, ILogger<AuthorController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetBookTypes()
    {
        try
        {
            var bookTypes = await _mediator.Send(new GetBookTypesQuery());
            return Ok(bookTypes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting book types");
            return StatusCode(500,"Error occurred while getting book types");
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetBookTypeById(int id)
    {
        try
        {
            var bookType = await _mediator.Send(new GetBookTypeByIdQuery {Id = id});
            return Ok(bookType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting book type by id");
            return StatusCode(500,"Error occurred while getting book type by id");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBookType([FromBody] CreateBookTypeCommand command)
    {
        try
        {
            var bookType = await _mediator.Send(command);
            return Ok(bookType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while creating book type");
            return StatusCode(500,"Error occurred while creating book type");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBookType(int id, [FromBody] UpdateBookTypeCommand command)
    {
        try
        {
            command.Id = id;
            var bookType = await _mediator.Send(command);
            return Ok(bookType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while updating book type");
            return StatusCode(500,"Error occurred while updating book type");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookType(int id)
    {
        try
        {
            var bookType = await _mediator.Send(new DeleteBookTypeCommand {Id = id});
            return Ok(bookType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while deleting book type");
            return StatusCode(500,"Error occurred while deleting book type");
        }
    }
}