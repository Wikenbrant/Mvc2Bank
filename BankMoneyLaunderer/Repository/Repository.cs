using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BankMoneyLaunderer.Models;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankMoneyLaunderer.Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Repository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<string>> GetCountriesAsync() =>
            await _context.Customers
                .Select(c => c.Country)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);

        public async Task<IEnumerable<CustomerReport>> GetCustomersTransactionsOverXAmountAsync(string country, decimal amount) =>
            await _context.Customers
                .Where(c=>c.Country == country)
                .Select(c=>new CustomerReport
                {
                    CustomerId = c.CustomerId,
                    Surname = c.Surname,
                    Givenname = c.Givenname,
                    SuspiciousAccounts = c.Dispositions.Select(d=>new AccountReport
                    {
                        AccountId = d.Account.AccountId,
                        SuspiciousTransactions = d.Account.Transactions
                            .Where(t => t.Amount > amount)
                            .Select(t=>new TransactionData
                        {
                            TransactionId = t.TransactionId,
                            Date = t.Date,
                            Amount = t.Amount
                        })
                    })
                })
                .ToListAsync()
                .ConfigureAwait(false);

        public async Task<IEnumerable<CustomerReport>> GetCustomersTransactionsOverXAmountLastXHoursAsync(
            string country, decimal amount, int hours) =>
            await _context.Customers
                .Where(c => c.Country == country)
                .Select(c => new CustomerReport
                {
                    CustomerId = c.CustomerId,
                    Surname = c.Surname,
                    Givenname = c.Givenname,
                    SuspiciousAccounts = c.Dispositions.Select(d => new AccountReport
                    {
                        AccountId = d.Account.AccountId,
                        SuspiciousTransactions = d.Account.Transactions
                            .Where(t => t.Amount > amount && t.Date > DateTime.Now.AddHours(hours * -1))
                            .Select(t => new TransactionData
                            {
                                TransactionId = t.TransactionId,
                                Date = t.Date,
                                Amount = t.Amount
                            })
                    })
                })
                .ToListAsync()
                .ConfigureAwait(false);

        public Task<MoneyLaundererReport> GetNextDateAsync() => 
            _context.MoneyLaundererReports
                .Where(r=>r.Succeeded)
                .OrderByDescending(r => r.EndDate)
                .Take(1)
                .FirstOrDefaultAsync();

        public Task<DateTime> GetOldestDateFromTransactionsAsync() => 
            _context.Transactions
                .OrderByDescending(t=>t.Date)
                .Take(1)
                .Select(t=>t.Date)
                .FirstOrDefaultAsync();

        public async Task<MoneyLaundererReport> CreateReportAsync(MoneyLaundererReport entity)
        {
            _context.MoneyLaundererReports.Add(entity);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }

        public async Task CompleteReportAsync(int id)
        {
            var report = await _context.MoneyLaundererReports.FirstOrDefaultAsync(r => r.MoneyLaundererId == id)
                .ConfigureAwait(false);

            report.Succeeded = true;

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public ValueTask DisposeAsync() => _context.DisposeAsync();

        void IDisposable.Dispose() => _context.Dispose();
    }
}