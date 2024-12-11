using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Users.Commands;

public class CreateUserCommand : IRequest<UserDto>
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Password { get; set; } = default!;
    
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public CreateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (userExist != null)
            {
                throw new ApplicationException("User already exist with this email");
            }
            var user = _mapper.Map<User>(request);
            await _unitOfWork.UserRepository.InsertUserAsync(user, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
    }
}