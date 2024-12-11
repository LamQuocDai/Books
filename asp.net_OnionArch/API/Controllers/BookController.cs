using Application.Features.Books.Commands;
using Application.Features.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthorController> _logger;
    
    public BookController(IMediator mediator, ILogger<AuthorController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetBooks()
    {
        try
        {
            var books = await _mediator.Send(new GetBooksQuery());
            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting books");
            return StatusCode(500,"Error occurred while getting books");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetBookById(int id)
    {
        try
        {
            var book = await _mediator.Send(new GetBookByIdQuery {Id = id});
            return Ok(book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting book by id");
            return StatusCode(500,"Error occurred while getting book by id");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
    {
        try
        {
            var book = await _mediator.Send(command);
            return Ok(book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while creating book");
            return StatusCode(500,"Error occurred while creating book");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookCommand command)
    {
        try
        {
            command.Id = id;
            var book = await _mediator.Send(command);
            return Ok(book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while updating book");
            return StatusCode(500,"Error occurred while updating book");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            var book = await _mediator.Send(new DeleteBookCommand {Id = id});
            return Ok(book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while deleting book");
            return StatusCode(500,"Error occurred while deleting book");
        }
    }
}