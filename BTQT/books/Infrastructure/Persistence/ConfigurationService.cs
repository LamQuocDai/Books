using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence.Context;


namespace Infrastructure.Persistence
{
    public static class ConfigurationService
    {
        public static void AutoMigration(this WebApplication webApplication)
        {
            using (var serviceScope = webApplication.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var configuration = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();
                context.Database.MigrateAsync().Wait();
                ApplicationDbContextSeed.SeedAsync(context, configuration).Wait();
            }
        }
    }
}