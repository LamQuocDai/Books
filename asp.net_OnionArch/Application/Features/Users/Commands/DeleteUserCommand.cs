using Application.DTOs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Users.Commands;

public class DeleteUserCommand : IRequest<UserDto>
{
    public int Id { get; set; }
    
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(User));
            await _unitOfWork.UserRepository.DeleteUserByIdAsync(request.Id, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
    }
}