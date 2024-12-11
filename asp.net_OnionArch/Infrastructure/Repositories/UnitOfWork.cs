using Domain.Interfaces.IRepositories;
using Persistence.Context;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IUserRepository UserRepository { get; private set; }
        public IBookTypeRepository BookTypeRepository { get; private set; }
        public IAuthorRepository AuthorRepository { get; private set; }
        public IBookRepository BookRepository { get; private set; }
        
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);
        }
        
        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
};