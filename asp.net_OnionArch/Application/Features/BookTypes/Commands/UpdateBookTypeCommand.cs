using Application.DTOs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.BookTypes.Commands;

public class UpdateBookTypeCommand : IRequest<BookTypeDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    
    public class UpdateBookTypeCommandHandler : IRequestHandler<UpdateBookTypeCommand, BookTypeDto>
    {
        private readonly IBookTypeRepository _bookTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public UpdateBookTypeCommandHandler(IBookTypeRepository bookTypeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _bookTypeRepository = bookTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<BookTypeDto> Handle(UpdateBookTypeCommand request, CancellationToken cancellationToken)
        {
            var bookType = await _bookTypeRepository.GetBookTypeByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(BookType));
            _mapper.Map(request, bookType);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<BookTypeDto>(bookType);
        }
    }
}