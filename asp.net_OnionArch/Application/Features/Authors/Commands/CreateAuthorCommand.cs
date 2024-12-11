using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Authors.Commands;

public class CreateAuthorCommand : IRequest<AuthorDto>
{
    public string Name { get; set; } = default!;
    
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, AuthorDto>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public CreateAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<AuthorDto> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = _mapper.Map<Author>(request);
            await _authorRepository.InsertAuthorAsync(author, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<AuthorDto>(author);
        }
    }
}