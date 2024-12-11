using Application.DTOs;
using Application.Exceptions;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Users.Queries;

public class LoginQuery : IRequest<UserDto>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public LoginQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            // Check if user exists
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email, cancellationToken) ??
                       throw new NotFoundException(nameof(User));

            // Check if password is correct
            if (user.Password != request.Password) throw new UnauthorizedAccessException("Password is incorrect");
            
            // Check user role
            var role = user.Email != "admin@admin.com" ? "User" : "Admin";

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Role = role
            };
        }
    }
}