using Application.DTOs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Authors.Queries;

public class GetAuthorByIdQuery : IRequest<AuthorDto>
{
    public int Id { get; set; }
    
    public class GetAuthorsByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        
        public GetAuthorsByIdQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }
        
        public async Task<AuthorDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Author));
            return _mapper.Map<AuthorDto>(author);
        }
    }
}