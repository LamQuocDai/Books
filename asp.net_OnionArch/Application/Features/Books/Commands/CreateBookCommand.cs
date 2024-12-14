using Application.DTOs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Books.Commands;

public class CreateBookCommand : IRequest<BookDto>
{
    public string Name { get; set; } = default!;
    public double Price { get; set; }
    public int AuthorId { get; set; }
    public int BookTypeId { get; set; }
    public DateOnly ReleaseDate { get; set; }
    
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public CreateBookCommandHandler( IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(request.AuthorId, cancellationToken) ?? throw new NotFoundException(nameof(Author));
            var bookType = await _unitOfWork.BookTypeRepository.GetBookTypeByIdAsync(request.BookTypeId, cancellationToken) ?? throw new NotFoundException(nameof(BookType));
            
            var book = _mapper.Map<Book>(request);
            await _unitOfWork.BookRepository.InsertBookAsync(book, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<BookDto>(book);
        }
    }
}