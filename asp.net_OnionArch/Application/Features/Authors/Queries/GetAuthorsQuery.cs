using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Authors.Queries;

public class GetAuthorsQuery : IRequest<IEnumerable<AuthorDto>>
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorDto>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public GetAuthorsQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _authorRepository.GetAuthorsAsync(cancellationToken);
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }
    }
}