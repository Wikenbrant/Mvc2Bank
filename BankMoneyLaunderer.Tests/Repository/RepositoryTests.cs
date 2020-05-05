using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Test;
using BankMoneyLaunderer.MoneyLaundryStrategy;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace BankMoneyLaunderer.Tests.Repository
{
    using static Testing;
    public class RepositoryTests : TestBase
    {
        public IMoneyLaundryConfig MoneyLaundryConfig => GetMoneyLaundryConfig();


        [Test]
        public async Task Should_Return_One_Given_Today_Date()
        {
            var customer = await AddAsync(new Customer
            {
                Country = "Sweden",
                Dispositions = new List<Disposition>
                {
                    new Disposition
                    {
                        Account = new Account
                        {
                            Balance = 11000,
                            Transactions = new List<Transaction>
                            {
                                new Transaction
                                {
                                    Balance = 16000,
                                    Date = DateTime.Today,
                                    Amount = 16000,
                                    Type = TransactionType.Credit.ToString()
                                },
                                new Transaction
                                {
                                    Balance = 5000,
                                    Date = DateTime.Today,
                                    Amount = 11000,
                                    Type = TransactionType.Debit.ToString()
                                }
                            }
                        }
                    }
                }
            });
            var repository = new MoneyLaundryRepository.Repository(GetContext(), GetMapper());

            var report = await repository.GetCustomersTransactionsOverXAmountAsync(DateTime.Today, "Sweden",
                MoneyLaundryConfig.MaximumSingelTransactionAmount);

            report.Should().NotBeEmpty();
            customer.CustomerId.Should().Be(report.FirstOrDefault().CustomerId);
            
        }

        [Test]
        public async Task Should_Return_None_Given_Today_Date()
        {
            var customer = await AddAsync(new Customer
            {
                Country = "Sweden",
                Dispositions = new List<Disposition>
                {
                    new Disposition
                    {
                        Account = new Account
                        {
                            Balance = 11000,
                            Transactions = new List<Transaction>
                            {
                                new Transaction
                                {
                                    Balance = 16000,
                                    Date = DateTime.Today,
                                    Amount = 16000,
                                    Type = TransactionType.Credit.ToString()
                                },
                                new Transaction
                                {
                                    Balance = 5000,
                                    Date = DateTime.Today,
                                    Amount = 11000,
                                    Type = TransactionType.Debit.ToString()
                                }
                            }
                        }
                    }
                }
            });
            var repository = new MoneyLaundryRepository.Repository(GetContext(), GetMapper());

            var report = await repository.GetCustomersTransactionsOverXAmountAsync(DateTime.Today.AddDays(-1), "Sweden",
                MoneyLaundryConfig.MaximumSingelTransactionAmount);

            report.Should().BeEmpty();
        }

        [Test]
        public async Task Should_Return_None_Given_Today_Date_And_72Hours()
        {
            var customer = await AddAsync(new Customer
            {
                Country = "Sweden",
                Dispositions = new List<Disposition>
                {
                    new Disposition
                    {
                        Account = new Account
                        {
                            Transactions = new List<Transaction>
                            {
                                new Transaction
                                {
                                    Balance = 50000,
                                    Date = DateTime.Today.AddDays(-5),
                                    Amount = 50000,
                                    Type = TransactionType.Credit.ToString()
                                },
                                new Transaction
                                {
                                    Balance = 50000,
                                    Date = DateTime.Today.AddDays(-5),
                                    Amount = 100000,
                                    Type = TransactionType.Debit.ToString()
                                },
                                new Transaction
                                {
                                    Balance = 16000,
                                    Date = DateTime.Today,
                                    Amount = 116000,
                                    Type = TransactionType.Credit.ToString()
                                },
                                new Transaction
                                {
                                    Balance = 5000,
                                    Date = DateTime.Today,
                                    Amount = 111000,
                                    Type = TransactionType.Debit.ToString()
                                }
                            }
                        }
                    }
                }
            });
            var repository = new MoneyLaundryRepository.Repository(GetContext(), GetMapper());

            var report = await repository.GetCustomersTransactionsOverXAmountLastXHoursAsync(DateTime.Today.AddDays(-1), "Sweden",
                MoneyLaundryConfig.MaximumXHoursTotalAmount,MoneyLaundryConfig.XHours);

            report.Should().BeEmpty();
        }

        [Test]
        public async Task Should_Return_One_Given_Today_Date_And_72Hours()
        {
            var customer = await AddAsync(new Customer
            {
                Country = "Sweden",
                Dispositions = new List<Disposition>
                {
                    new Disposition
                    {
                        Account = new Account
                        {
                            Balance = 35000,
                            Transactions = new List<Transaction>
                            {
                                new Transaction
                                {
                                    Balance = 4000,
                                    Date = DateTime.Today.AddDays(-4),
                                    Amount = 4000,
                                    Type = TransactionType.Credit.ToString()
                                },
                                new Transaction
                                {
                                    Balance = 16000,
                                    Date = DateTime.Today,
                                    Amount = 20000,
                                    Type = TransactionType.Credit.ToString()
                                },
                                new Transaction
                                {
                                    Balance = 35000,
                                    Date = DateTime.Today,
                                    Amount = 15000,
                                    Type = TransactionType.Credit.ToString()
                                }
                            }
                        }
                    }
                }
            });
            var repository = new MoneyLaundryRepository.Repository(GetContext(), GetMapper());

            var report = await repository.GetCustomersTransactionsOverXAmountLastXHoursAsync(DateTime.Today, "Sweden",
                MoneyLaundryConfig.MaximumXHoursTotalAmount, MoneyLaundryConfig.XHours);

            report.Should().NotBeEmpty();
            customer.CustomerId.Should().Be(report.FirstOrDefault().CustomerId);
        }
    }
}