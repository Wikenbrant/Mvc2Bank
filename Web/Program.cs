using System;
using System.Threading.Tasks;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await EnsureDatabaseAsync(host).ConfigureAwait(false);

            await host.RunAsync().ConfigureAwait(false);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static async Task EnsureDatabaseAsync(IHost host)
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await ApplicationDbContextSeed.SeedDefaultRolesAsync(roleManager).ConfigureAwait(false);
                await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                logger.LogError(e, "An error occurred while migrating or seeding the database.");
                throw;
            }
        }
    }



    
}
