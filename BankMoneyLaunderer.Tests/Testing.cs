using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using BankMoneyLaunderer;
using BankMoneyLaunderer.MoneyLaundryStrategy;
using Infrastructure.Persistence;

[SetUpFixture]
public class Testing
{
    private static IConfigurationRoot _configuration;
    private static IServiceScopeFactory _scopeFactory;
    private static IServiceProvider _provider;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true);

        _configuration = builder.Build();


        

        var startup = new Startup(_configuration);

        var services = new ServiceCollection();

        startup.ConfigureServices(services);


        _provider = services.BuildServiceProvider();
        _scopeFactory = _provider.GetService<IServiceScopeFactory>();

    }

    public static IMapper GetMapper()
    {
        return _provider.GetRequiredService<IMapper>();
    }

    public static ApplicationDbContext GetContext()
    {
        return _provider.GetRequiredService<ApplicationDbContext>();
    }

    public static IMoneyLaundryConfig GetMoneyLaundryConfig()
    {
        return _provider.GetRequiredService<IMoneyLaundryConfig>();
    }
    public static Task ResetStateAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return context.Database.EnsureDeletedAsync();

    }

    public static async Task<TEntity> FindAsync<TEntity>(int id)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(id).ConfigureAwait(false);
    }

    public static async Task<TEntity> AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync().ConfigureAwait(false);

        return entity;
    }
    public static async Task<IEnumerable<TEntity>> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        context.AddRange(entities);

        await context.SaveChangesAsync().ConfigureAwait(false);

        return entities;
    }
    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
    }
}
