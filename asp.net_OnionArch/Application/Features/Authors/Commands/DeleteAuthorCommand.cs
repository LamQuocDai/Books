using Application.DTOs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Authors.Commands;

public class DeleteAuthorCommand : IRequest<AuthorDto>
{
    public int Id { get; set; }
    
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, AuthorDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteAuthorCommandHandler( IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<AuthorDto> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Author));
            await  _unitOfWork.AuthorRepository.DeleteAuthorByIdAsync(request.Id, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<AuthorDto>(author);
        }
    }
}