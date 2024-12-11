namespace Domain.Interfaces.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IBookTypeRepository BookTypeRepository { get; }
    IAuthorRepository AuthorRepository { get; }
    IBookRepository BookRepository { get; }
    Task<int> CompleteAsync(CancellationToken cancellationToken);
}