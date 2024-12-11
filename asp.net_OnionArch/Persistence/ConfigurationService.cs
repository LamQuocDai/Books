using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using System.Threading.Tasks;

namespace Persistence
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