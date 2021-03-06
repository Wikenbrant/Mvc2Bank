﻿using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> , IApplicationDbContext
    {
        private readonly ISearchService _searchService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,ISearchService searchService)
            : base(options)
        {
            _searchService = searchService;
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Disposition> Dispositions { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<PermanentOrder> PermanentOrder { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<MoneyLaundererReport> MoneyLaundererReports { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }));
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (!Database.IsInMemory())
            {
                var createdOrUpdated = ChangeTracker.Entries<Customer>()
                    .Where(e => e.Entity is Customer &&
                                (e.State == EntityState.Added || e.State == EntityState.Modified))
                    .Select(e => e.Entity)
                    .ToArray();

                await _searchService.CreateOrUpdateCustomers(createdOrUpdated).ConfigureAwait(false);


                var deleted = ChangeTracker.Entries<Customer>()
                    .Where(e => e.Entity is Customer &&
                                e.State == EntityState.Deleted)
                    .Select(e => e.Entity)
                    .ToArray();

                await _searchService.DeleteCustomers(deleted).ConfigureAwait(false);
            }
            
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

    }
}
