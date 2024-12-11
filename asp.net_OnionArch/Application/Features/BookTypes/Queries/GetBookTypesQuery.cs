using Application.DTOs;
using AutoMapper;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.BookTypes.Queries;

public class GetBookTypesQuery : IRequest<IEnumerable<BookTypeDto>>
{
    public class GetBookTypesQueryHandler : IRequestHandler<GetBookTypesQuery, IEnumerable<BookTypeDto>>
    {
        private readonly IBookTypeRepository _bookTypeRepository;
        private readonly IMapper _mapper;

        public GetBookTypesQueryHandler(IBookTypeRepository bookTypeRepository, IMapper mapper)
        {
            _bookTypeRepository = bookTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookTypeDto>> Handle(GetBookTypesQuery request, CancellationToken cancellationToken)
        {
            var bookTypes = await _bookTypeRepository.GetBookTypesAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BookTypeDto>>(bookTypes);
        }
    }
}