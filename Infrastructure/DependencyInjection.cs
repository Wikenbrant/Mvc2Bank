using Application.Common.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            //{
            //    services.AddDbContext<ApplicationDbContext>(options =>
            //    {

            //        options.UseInMemoryDatabase("Default");
            //    });
            //}
            //else
            //{
            //    services.AddDbContext<ApplicationDbContext>(options =>
            //        options.UseSqlServer(
            //            configuration.GetConnectionString("Default"),
            //            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            //}
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var x = configuration.GetConnectionString("Default");
                options.UseSqlServer(
                    configuration.GetConnectionString("Default"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();


            services.AddTransient<IIdentityService, IdentityService>();


            services.AddAuthentication().AddJwtBearer();

            return services;
        }
    }
}
