using Domain.Interfaces;

namespace Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository _userRepository { get; }
    IBookTypeRepository _bookTypeRepository { get; }
    IAuthorRepository _authorRepository { get; }
    IBookRepository _bookRepository { get; }
    
    Task<int> CompleteAsync();
    Task<int> CompleteAsync(CancellationToken cancellationToken);
}