using Application.DTOs;
using AutoMapper;
using Domain.Interfaces.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries;
public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserRepository.GetUsersAsync(cancellationToken);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
