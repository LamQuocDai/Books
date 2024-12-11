using Application.DTOs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Authors.Commands;

public class UpdateAuthorCommand : IRequest<AuthorDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, AuthorDto>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<AuthorDto> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Author));
            _mapper.Map(request, author);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<AuthorDto>(author);
        }
    }
}