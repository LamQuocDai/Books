﻿using Application.Features.Users.Commands;
using Domain.Interfaces.IRepositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Validators;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateUserCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .MustAsync(UserExists).WithMessage("User not found.");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
        
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required.");
    }
    
    private async Task<bool> UserExists(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.UserRepository.GetUserByIdAsync(id, cancellationToken) != null;
    }
}