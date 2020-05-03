using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankMoneyLaunderer.Models;

namespace BankMoneyLaunderer.MoneyLaundryStrategy
{
    public interface IMoneyLaundryStrategy
    {
        public string ReportName { get; }
        public Task<IEnumerable<CustomerReport>> GenerateReportAsync(DateTime date,string country);
    }
}