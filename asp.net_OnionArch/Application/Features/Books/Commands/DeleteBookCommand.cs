using Application.DTOs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Books.Commands;

public class DeleteBookCommand : IRequest<BookDto>
{
    public int Id { get; set; }
    
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteBookCommandHandler(IBookRepository bookRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<BookDto> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Book));
            await _bookRepository.DeleteBookByIdAsync(request.Id, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<BookDto>(book);
        }
    }
}