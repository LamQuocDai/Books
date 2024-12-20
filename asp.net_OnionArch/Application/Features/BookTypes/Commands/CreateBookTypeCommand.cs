﻿using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.BookTypes.Commands;

public class CreateBookTypeCommand : IRequest<BookTypeDto>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    
    public class CreateBookTypeCommandHandler : IRequestHandler<CreateBookTypeCommand, BookTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public CreateBookTypeCommandHandler( IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<BookTypeDto> Handle(CreateBookTypeCommand request, CancellationToken cancellationToken)
        {
            var bookType = _mapper.Map<BookType>(request);
            await _unitOfWork.BookTypeRepository.InsertBookTypeAsync(bookType, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<BookTypeDto>(bookType);
        }
    }
}