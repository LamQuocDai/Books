using Application.DTOs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.BookTypes.Commands;

public class DeleteBookTypeCommand : IRequest<BookTypeDto>
{
    public int Id { get; set; }
    
    public class DeleteBookTypeCommandHandler : IRequestHandler<DeleteBookTypeCommand, BookTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteBookTypeCommandHandler(IBookTypeRepository bookTypeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<BookTypeDto> Handle(DeleteBookTypeCommand request, CancellationToken cancellationToken)
        {
            var bookType = await _unitOfWork.BookTypeRepository.GetBookTypeByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(BookType));
            await _unitOfWork.BookTypeRepository.DeleteBookTypeByIdAsync(request.Id, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<BookTypeDto>(bookType);
        }
    }
}