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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public UpdateBookCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository.GetBookByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Book));
            var author = await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(request.AuthorId, cancellationToken) ?? throw new NotFoundException(nameof(Author));
            var bookType = await _unitOfWork.BookTypeRepository.GetBookTypeByIdAsync(request.BookTypeId, cancellationToken) ?? throw new NotFoundException(nameof(BookType));
            _mapper.Map(request, book);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<BookDto>(book);
        }
    }
}