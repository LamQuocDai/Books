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
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookTypeRepository _bookTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public CreateBookCommandHandler(IBookRepository bookRepository,IAuthorRepository authorRepository, IBookTypeRepository bookTypeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _bookTypeRepository = bookTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(request.AuthorId, cancellationToken) ?? throw new NotFoundException(nameof(Author));
            var bookType = await _bookTypeRepository.GetBookTypeByIdAsync(request.BookTypeId, cancellationToken) ?? throw new NotFoundException(nameof(BookType));
            
            var book = _mapper.Map<Book>(request);
            await _bookRepository.InsertBookAsync(book, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<BookDto>(book);
        }
    }
}