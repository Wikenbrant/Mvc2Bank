using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankMoneyLaunderer.Models;
using Domain.Entities;

namespace BankMoneyLaunderer.MoneyLaundryRepository
{
    public interface IRepository : IAsyncDisposable,IDisposable
    {
        Task<IEnumerable<string>> GetCountriesAsync();

        Task<IEnumerable<CustomerReport>> GetCustomersTransactionsOverXAmountAsync(DateTime date, string country, decimal amount);

        Task<IEnumerable<CustomerReport>> GetCustomersTransactionsOverXAmountLastXHoursAsync(
            DateTime date, string country, decimal amount, int hours);

        Task<MoneyLaundererReport> GetNextDateAsync();

        Task<DateTime> GetOldestDateFromTransactionsAsync();

        Task<MoneyLaundererReport> CreateReportAsync(MoneyLaundererReport entity);

        Task CompleteReportAsync(int id);
    }
}