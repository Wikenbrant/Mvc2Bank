using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankMoneyLaunderer.Models;
using BankMoneyLaunderer.Repository;

namespace BankMoneyLaunderer.MoneyLaundryStrategy
{
    public class TransactionBiggerThanXkrLastXHours : IMoneyLaundryStrategy
    {
        private readonly IMoneyLaundryConfig _config;
        private readonly IRepository _repository;

        public TransactionBiggerThanXkrLastXHours(IMoneyLaundryConfig config, IRepository repository)
        {
            _config = config;
            _repository = repository;
        }
        public string ReportName => $"Transaction Bigger Than {_config.MaximumSingelTransactionAmount}kr Last {_config.XHours}h";

        public Task<IEnumerable<CustomerReport>> GenerateReportAsync(DateTime date, string country) =>
            _repository.GetCustomersTransactionsOverXAmountLastXHoursAsync(country, _config.MaximumXHoursTotalAmount,
                _config.XHours);
    }
}