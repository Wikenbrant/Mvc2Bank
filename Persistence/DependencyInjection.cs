using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {

            //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            //{
            //    services.AddDbContext<ApplicationDbContext>(options =>
            //        options.UseInMemoryDatabase("DefaultMemory"));
            //}
            //else
            //{
            //    services.AddDbContext<ApplicationDbContext>(options =>
            //        options.UseSqlServer(
            //            configuration.GetConnectionString("Default"),
            //            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            //}
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Default"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            return services;
        }
    }
}
