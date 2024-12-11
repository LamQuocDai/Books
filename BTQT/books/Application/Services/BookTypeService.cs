using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class BookTypeService: IBookTypeService
{
    private readonly IBookTypeRepository _bookTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public BookTypeService(IBookTypeRepository bookTypeRepository, IUnitOfWork unitOfWork)
    {
        _bookTypeRepository = bookTypeRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<BookTypeDto> GetBookTypeByIdAsync(int id)
    {
        var bookType = await _bookTypeRepository.GetBookTypeByIdAsync(id);
        
        return new BookTypeDto
        {
            Id = bookType.Id,
            Name = bookType.Name,
            Description = bookType.Description
        };
    }
    
    public async Task<IEnumerable<BookTypeDto>> GetAllBookTypesAsync()
    {
        var bookTypes = await _bookTypeRepository.GetAllBookTypesAsync();
        
        return bookTypes.Select(bookType => new BookTypeDto
        {
            Id = bookType.Id,
            Name = bookType.Name,
            Description = bookType.Description
        });
    }
    
    public async Task<BookTypeDto> CreateBookTypeAsync(BookTypeDto bookTypeDto, CancellationToken cancellationToken)
    {
        var bookType = new BookType()
        {
            Name = bookTypeDto.Name,
            Description = bookTypeDto.Description
        };
        
        await _bookTypeRepository.CreateBookTypeAsync(bookType);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return new BookTypeDto
        {
            Id = bookType.Id,
            Name = bookType.Name,
            Description = bookType.Description
        };
    }
    
    public async Task<BookTypeDto> UpdateBookTypeAsync(BookTypeDto bookTypeDto, CancellationToken cancellationToken)
    {
        var bookType = await _bookTypeRepository.GetBookTypeByIdAsync(bookTypeDto.Id);
        
        bookType.Name = bookTypeDto.Name;
        bookType.Description = bookTypeDto.Description;
        
        await _bookTypeRepository.UpdateBookTypeAsync(bookType);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return new BookTypeDto
        {
            Id = bookType.Id,
            Name = bookType.Name,
            Description = bookType.Description
        };
    }
    
    public async Task<BookTypeDto> DeleteBookTypeAsync(int id, CancellationToken cancellationToken)
    {
        var bookType = await _bookTypeRepository.DeleteBookTypeAsync(id);
        await _unitOfWork.CompleteAsync(cancellationToken);
        
        return new BookTypeDto
        {
            Id = bookType.Id,
            Name = bookType.Name,
            Description = bookType.Description
        };
    }
}