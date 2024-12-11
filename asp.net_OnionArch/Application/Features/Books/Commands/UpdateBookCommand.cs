using Application.DTOs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Books.Commands;

public class UpdateBookCommand : IRequest<BookDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public double Price { get; set; }
    public int AuthorId { get; set; }
    public int BookTypeId { get; set; }
    public DateOnly ReleaseDate { get; set; }
    
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDto>
    {
        
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookTypeRepository _bookTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public UpdateBookCommandHandler(IBookRepository bookRepository,IAuthorRepository authorRepository, IBookTypeRepository bookTypeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _bookTypeRepository = bookTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Book));
            var author = await _authorRepository.GetAuthorByIdAsync(request.AuthorId, cancellationToken) ?? throw new NotFoundException(nameof(Author));
            var bookType = await _bookTypeRepository.GetBookTypeByIdAsync(request.BookTypeId, cancellationToken) ?? throw new NotFoundException(nameof(BookType));
            _mapper.Map(request, book);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<BookDto>(book);
        }
    }
}