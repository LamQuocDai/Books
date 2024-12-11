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
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<AuthorDto> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Author));
            await _authorRepository.DeleteAuthorByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<AuthorDto>(author);
        }
    }
}