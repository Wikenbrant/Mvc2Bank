using System.Reflection;
using AutoMapper;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Search.Repository;

namespace Search
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetCallingAssembly());

            if (_configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {

                    options.UseInMemoryDatabase("Default");
                });
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        _configuration.GetConnectionString("Default"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddTransient<IRepository, Repository.Repository>();
        }
    }
}