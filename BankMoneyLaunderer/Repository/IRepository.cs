using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankMoneyLaunderer.Models;
using Domain.Entities;

namespace BankMoneyLaunderer.Repository
{
    public interface IRepository : IAsyncDisposable,IDisposable
    {
        Task<IEnumerable<string>> GetCountriesAsync();

        Task<IEnumerable<CustomerReport>> GetCustomersTransactionsOverXAmountAsync(string country, decimal amount);

        Task<IEnumerable<CustomerReport>> GetCustomersTransactionsOverXAmountLastXHoursAsync(string country,
            decimal amount, int hours);

        Task<MoneyLaundererReport> GetNextDateAsync();

        Task<DateTime> GetOldestDateFromTransactionsAsync();

        Task<MoneyLaundererReport> CreateReportAsync(MoneyLaundererReport entity);

        Task CompleteReportAsync(int id);
    }
}