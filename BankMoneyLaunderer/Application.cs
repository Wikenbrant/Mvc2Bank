using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankMoneyLaunderer.Models;
using BankMoneyLaunderer.MoneyLaundryRepository;
using BankMoneyLaunderer.MoneyLaundryStrategy;
using BankMoneyLaunderer.Services;
using Domain.Entities;

namespace BankMoneyLaunderer
{
    public class Application
    {
        private readonly IRepository _repository;
        private readonly IEnumerable<IMoneyLaundryStrategy> _strategies;
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly IEmailService _emailService;

        public Application(IRepository repository,IEnumerable<IMoneyLaundryStrategy> strategies,
            IEmailConfiguration emailConfiguration,IEmailService emailService)
        {
            _repository = repository;
            _strategies = strategies;
            _emailConfiguration = emailConfiguration;
            _emailService = emailService;
        }

        public async Task RunAsync()
        {
            var startDate = await GetStartDateAsync().ConfigureAwait(false);

            var countries = (await _repository.GetCountriesAsync().ConfigureAwait(false)).ToList();

            for (var date = startDate; date.Date <= DateTime.Today; date = date.AddDays(1))
            {
                var report = await _repository.CreateReportAsync(new MoneyLaundererReport
                    { StartDate = date, EndDate = date }).ConfigureAwait(false);
                foreach (var country in countries)
                {
                    var email = await CreateEmailAsync(date, country).ConfigureAwait(false);
                    _emailService.Send(email);
                }
                await _repository.CompleteReportAsync(report.MoneyLaundererId).ConfigureAwait(false);
            }

        }

        public async Task<DateTime> GetStartDateAsync()
        {
            var lastReport = await _repository.GetNextDateAsync().ConfigureAwait(false);

            if (lastReport == null) return await _repository.GetOldestDateFromTransactionsAsync().ConfigureAwait(false);

            return lastReport.EndDate.AddDays(1);
        }

        public async Task<EmailMessage> CreateEmailAsync(DateTime date, string country)
        {
            var from = new List<EmailAddress>
            {
                new EmailAddress{Name = "TheMoneyLaunderer",Address = _emailConfiguration.EmailAddress}
            };
            var to = new List<EmailAddress>
            {
                new EmailAddress{Name = country,Address = $"{country}@testbanken.se" }
            };

            var text = await CreateTextAsync(date,country).ConfigureAwait(false);

            return new EmailMessage
            {
                ToAddresses = to,
                FromAddresses = from,
                Content = text,
                Subject = $"{date} : Money Launder Report {country}"
            };
        }

        public async Task<string> CreateTextAsync(DateTime date, string country)
        {
            var sb = new StringBuilder();
            sb.Append(date);
            sb.Append($"{country}: ");
            sb.AppendLine();

            foreach(var (strategy ,report) in await GetCustomerReportsAsync(date,country).ConfigureAwait(false))
            {
                sb.Append($"Report: {strategy.ReportName}");
                foreach (var customer in report)
                {
                    sb.Append($"    {customer.CustomerId}. {customer.Givenname} {customer.Surname}");
                    sb.Append($"    Accounts: ");

                    foreach (var account in customer.SuspiciousAccounts)
                    {
                        sb.Append($"        {account.AccountId}. Transactions: ");
                        foreach (var transaction in account.SuspiciousTransactions)
                        {
                            sb.Append($"            {transaction.TransactionId}. Amount:{transaction.Amount} Date:{transaction.Date}");
                        }
                    }
                }
            }
             

            return sb.ToString();
        }

        public async  Task<IEnumerable<(IMoneyLaundryStrategy,IEnumerable<CustomerReport>)>> GetCustomerReportsAsync(DateTime date, string country)
        {
            var reports = new List<(IMoneyLaundryStrategy, IEnumerable<CustomerReport>)>();
            foreach (var strategy in _strategies)
            {
                reports.Add((strategy, (await strategy.GenerateReportAsync(date, country).ConfigureAwait(false)).ToList()));
            }

            return reports.ToList();
        }
    }
}