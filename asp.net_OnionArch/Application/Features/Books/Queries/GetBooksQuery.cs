using Application.DTOs;
using AutoMapper;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Books.Queries;

public class GetBooksQuery : IRequest<IEnumerable<BookDto>>
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _unitOfWork.BookRepository.GetBooksAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }
    }
}