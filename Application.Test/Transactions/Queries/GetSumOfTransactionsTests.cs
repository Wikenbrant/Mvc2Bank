using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Transactions.Queries.GetSumOfTransactions;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Application.Test.Transactions.Queries
{
    using static Testing;
    public class GetSumOfTransactionsTests : TestBase
    {
        [Test]
        public async Task ShouldReturnSumOfTransactions()
        {
            await AddRangeAsync(new List<Transaction>
            {
                new Transaction {Amount = 700},
                new Transaction {Amount = 900},
                new Transaction {Amount = 1000},
                new Transaction {Amount = 600}
            });

            var query = new GetSumOfTransactionsQuery();

            var result = await SendAsync(query);

            result.Should().Be(3200m);
        }

        [Test]
        public async Task ShouldReturn0()
        {
            var query = new GetSumOfTransactionsQuery();

            var result = await SendAsync(query);

            result.Should().Be(0m);
        }
    }
}