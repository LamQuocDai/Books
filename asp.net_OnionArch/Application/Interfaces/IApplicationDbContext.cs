using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {

        DbSet<Cart> Carts { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<BookCatalogue> BookCatalogues { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<BookType> BookTypes { get; set; }
        DbSet<CartDetail> CartDetails { get; set; }
        DbSet<Catalogue> Catalogues { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrdersDetail { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
