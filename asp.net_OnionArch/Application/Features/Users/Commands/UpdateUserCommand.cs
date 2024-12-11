using Application.DTOs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Users.Commands;

public class UpdateUserCommand : IRequest<UserDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Phone { get; set; } = default!;
    
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(User));
            _mapper.Map(request, user);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
    }
}