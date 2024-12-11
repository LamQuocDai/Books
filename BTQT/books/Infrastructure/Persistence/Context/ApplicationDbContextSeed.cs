using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Context
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, IConfiguration configuration)
        {
            if (!await context.Users.AnyAsync())
            {
                var defaultUser = configuration.GetSection("DefaultUser").Get<User>();
                context.Users.Add(defaultUser);
            }

            if (!await context.Authors.AnyAsync())
            {
                var defaultAuthor = configuration.GetSection("DefaultAuthor").Get<Author>();
                context.Authors.Add(defaultAuthor);
            }

            if (!await context.BookTypes.AnyAsync())
            {
                var defaultBookType = configuration.GetSection("DefaultBookType").Get<BookType>();
                context.BookTypes.Add(defaultBookType);
            }

            if (!await context.Books.AnyAsync())
            {
                var defaultBook = configuration.GetSection("DefaultBook").Get<Book>();
                context.Books.Add(defaultBook);
            }

            if (!await context.Carts.AnyAsync())
            {
                var defaultCart = configuration.GetSection("DefaultCart").Get<Cart>();
                context.Carts.Add(defaultCart);
            }

            if (!await context.CartDetails.AnyAsync())
            {
                var defaultCartDetail = configuration.GetSection("DefaultCartDetail").Get<CartDetail>();
                context.CartDetails.Add(defaultCartDetail);
            }

            if (!await context.Catalogues.AnyAsync())
            {
                var defaultCatalogue = configuration.GetSection("DefaultCatalogue").Get<Catalogue>();
                context.Catalogues.Add(defaultCatalogue);
            }

            if (!await context.BookCatalogues.AnyAsync())
            {
                var defaultBookCatalogue = configuration.GetSection("DefaultBookCatalogue").Get<BookCatalogue>();
                context.BookCatalogues.Add(defaultBookCatalogue);
            }

            if (!await context.Orders.AnyAsync())
            {
                var defaultOrder = configuration.GetSection("DefaultOrder").Get<Order>();
                context.Orders.Add(defaultOrder);
            }

            if (!await context.OrdersDetail.AnyAsync())
            {
                var defaultOrderDetail = configuration.GetSection("DefaultOrderDetail").Get<OrderDetail>();
                context.OrdersDetail.Add(defaultOrderDetail);
            }

            await context.SaveChangesAsync();
        }
    }
}