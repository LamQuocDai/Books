using Application.DTOs;
using AutoMapper;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.BookTypes.Queries;

public class GetBookTypesQuery : IRequest<IEnumerable<BookTypeDto>>
{
    public class GetBookTypesQueryHandler : IRequestHandler<GetBookTypesQuery, IEnumerable<BookTypeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBookTypesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookTypeDto>> Handle(GetBookTypesQuery request, CancellationToken cancellationToken)
        {
            var bookTypes = await _unitOfWork.BookTypeRepository.GetBookTypesAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BookTypeDto>>(bookTypes);
        }
    }
}