using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence.Context;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookCatalogue> BookCatalogues { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookType> BookTypes { get; set; }
    public DbSet<CartDetail> CartDetails { get; set; }
    public DbSet<Catalogue> Catalogues { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrdersDetail { get; set; }
    public DbSet<User> Users { get; set; }
    
}