using Application.DTOs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.BookTypes.Queries;

public class GetBookTypeByIdQuery : IRequest<BookTypeDto>
{
    public int Id { get; set; }
    
    public class GetBookTypeByIdQueryHandler : IRequestHandler<GetBookTypeByIdQuery, BookTypeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public GetBookTypeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<BookTypeDto> Handle(GetBookTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var bookType = await _unitOfWork.BookTypeRepository.GetBookTypeByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(BookType));
            return _mapper.Map<BookTypeDto>(bookType);
        }
    }
}