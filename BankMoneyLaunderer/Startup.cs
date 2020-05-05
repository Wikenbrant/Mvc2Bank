using System.Reflection;
using Application;
using Application.Common.Interfaces;
using AutoMapper;
using BankMoneyLaunderer.MoneyLaundryRepository;
using BankMoneyLaunderer.MoneyLaundryStrategy;
using BankMoneyLaunderer.Services;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankMoneyLaunderer
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

            services.AddTransient<IEmailConfiguration>(provider =>
                _configuration.GetSection(nameof(EmailConfiguration)).Get<EmailConfiguration>());
            services.AddTransient<IEmailService, EmailService>();

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

            services.AddTransient<IRepository, MoneyLaundryRepository.Repository>();

            services.AddTransient<IMoneyLaundryConfig>(provider => _configuration.GetSection(nameof(MoneyLaundryConfig)).Get<MoneyLaundryConfig>());
            services.AddTransient<IMoneyLaundryStrategy, TransactionBiggerThanXkrStrategy>();
            services.AddTransient<IMoneyLaundryStrategy, TransactionBiggerThanXkrLastXHours>();

            services.AddSingleton<Application>();
        }
    }
}