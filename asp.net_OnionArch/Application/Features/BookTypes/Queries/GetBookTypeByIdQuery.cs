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
        private readonly IBookTypeRepository _bookTypeRepository;
        private readonly IMapper _mapper;
        
        public GetBookTypeByIdQueryHandler(IBookTypeRepository bookTypeRepository, IMapper mapper)
        {
            _bookTypeRepository = bookTypeRepository;
            _mapper = mapper;
        }
        
        public async Task<BookTypeDto> Handle(GetBookTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var bookType = await _bookTypeRepository.GetBookTypeByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(BookType));
            return _mapper.Map<BookTypeDto>(bookType);
        }
    }
}