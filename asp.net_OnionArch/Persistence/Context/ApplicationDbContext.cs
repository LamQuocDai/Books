using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
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
        
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                foreach (var property in entry.Properties)
                {
                    if (property.Metadata.ClrType == typeof(DateTime) ||
                        property.Metadata.ClrType == typeof(DateTime?))
                    {
                        if (property.CurrentValue is DateTime dateTime && dateTime.Kind == DateTimeKind.Local)
                        {
                            property.CurrentValue = dateTime.ToUniversalTime();
                        }
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
