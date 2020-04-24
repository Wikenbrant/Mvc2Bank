﻿using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextFactory :IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var connectionString = configuration
                .GetConnectionString("Default");

            dbContextBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(dbContextBuilder.Options);
        }
    }
}