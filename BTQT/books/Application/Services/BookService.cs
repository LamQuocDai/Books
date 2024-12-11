using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class BookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<BookDto> GetBookByIdAsync(int id)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        
        return new BookDto
        {
            Id = book.Id,
            Name = book.Name,
            Price = book.Price,
            AuthorId = book.AuthorId,
            BookTypeId = book.BookTypeId,
            ReleaseDate = book.ReleaseDate
        };
    }
    
    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllBooksAsync();
        
        return books.Select(book => new BookDto
        {
            Id = book.Id,
            Name = book.Name,
            Price = book.Price,
            AuthorId = book.AuthorId,
            BookTypeId = book.BookTypeId,
            ReleaseDate = book.ReleaseDate
        });
    }
    
    public async Task<BookDto> CreateBookAsync(BookDto bookDto, CancellationToken cancellationToken)
    {
        var book = new Book()
        {
            Name = bookDto.Name,
            Price = bookDto.Price,
            AuthorId = bookDto.AuthorId,
            BookTypeId = bookDto.BookTypeId,
            ReleaseDate = bookDto.ReleaseDate
        };
        
        await _bookRepository.CreateBookAsync(book);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return new BookDto
        {
            Id = book.Id,
            Name = book.Name,
            Price = book.Price,
            AuthorId = book.AuthorId,
            BookTypeId = book.BookTypeId,
            ReleaseDate = book.ReleaseDate
        };
    }
    
    public async Task<BookDto> UpdateBookAsync(BookDto bookDto, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookByIdAsync(bookDto.Id);
        
        book.Name = bookDto.Name;
        book.Price = bookDto.Price;
        book.AuthorId = bookDto.AuthorId;
        book.BookTypeId = bookDto.BookTypeId;
        book.ReleaseDate = bookDto.ReleaseDate;
        
        await _bookRepository.UpdateBookAsync(book);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return new BookDto
        {
            Id = book.Id,
            Name = book.Name,
            Price = book.Price,
            AuthorId = book.AuthorId,
            BookTypeId = book.BookTypeId,
            ReleaseDate = book.ReleaseDate
        };
    }
    
    public async Task<BookDto> DeleteBookAsync(int id, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.DeleteBookAsync(id);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return new BookDto
        {
            Id = book.Id,
            Name = book.Name,
            Price = book.Price,
            AuthorId = book.AuthorId,
            BookTypeId = book.BookTypeId,
            ReleaseDate = book.ReleaseDate
        };
    }
}