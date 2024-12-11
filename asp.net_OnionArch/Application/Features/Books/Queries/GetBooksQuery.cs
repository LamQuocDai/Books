using Application.DTOs;
using AutoMapper;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Books.Queries;

public class GetBooksQuery : IRequest<IEnumerable<BookDto>>
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetBooksAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }
    }
}