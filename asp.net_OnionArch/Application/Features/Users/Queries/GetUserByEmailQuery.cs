using Application.DTOs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Users.Queries;

public class GetUserByEmailQuery : IRequest<UserDto>
{
    public string Email { get; set; }
    
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        
        public GetUserByEmailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email, cancellationToken) ?? throw new NotFoundException(nameof(User));
            return _mapper.Map<UserDto>(user);
        }
    }
}