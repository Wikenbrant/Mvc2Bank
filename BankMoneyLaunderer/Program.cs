using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BankMoneyLaunderer.Models;
using BankMoneyLaunderer.Repository;
using BankMoneyLaunderer.Services;
using Domain.Entities;
using Domain.SearchModels;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankMoneyLaunderer
{
    class Program
    {
        static Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var collection = new ServiceCollection();
            collection.AddSingleton<IConfiguration>(configuration);
            var startup = new Startup(configuration);
            startup.ConfigureServices(collection);

            var provider = collection.BuildServiceProvider();

            var app = provider.GetRequiredService<Application>();

            return app.RunAsync();
        }
    }
}
