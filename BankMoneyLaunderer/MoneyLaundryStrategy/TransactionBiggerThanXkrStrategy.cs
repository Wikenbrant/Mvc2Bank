using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankMoneyLaunderer.Models;
using BankMoneyLaunderer.Repository;

namespace BankMoneyLaunderer.MoneyLaundryStrategy
{
    public class TransactionBiggerThanXkrStrategy : IMoneyLaundryStrategy
    {
        private readonly IMoneyLaundryConfig _config;
        private readonly IRepository _repository;

        public TransactionBiggerThanXkrStrategy(IMoneyLaundryConfig config,IRepository repository)
        {
            _config = config;
            _repository = repository;
        }

        public string ReportName => $"Transaction Bigger Than {_config.MaximumSingelTransactionAmount}kr";

        public Task<IEnumerable<CustomerReport>> GenerateReportAsync(DateTime date,string country) =>
            _repository.GetCustomersTransactionsOverXAmountAsync(country,
                _config.MaximumSingelTransactionAmount);
    }
}