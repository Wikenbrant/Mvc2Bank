using System.Threading.Tasks;
using Application.Transactions.Queries.GetTransaction;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Application.Test.Transactions.Queries
{
    using static Testing;
    public class GetTransactionTests: TestBase
    {
        [Test]
        public async Task ShouldGetTransaction()
        {
            await AddAsync(new Transaction()).ConfigureAwait(false);

            var query = new GetTransactionQuery{Id = 1};

            var transaction = await SendAsync(query).ConfigureAwait(false);

            transaction.Should().NotBeNull();
        }

        [Test]
        public async Task ShouldNotGetTransaction()
        {
            await AddAsync(new Transaction()).ConfigureAwait(false);

            var query = new GetTransactionQuery { Id = 2 };

            var model = await SendAsync(query).ConfigureAwait(false);

            model.Transaction.Should().BeNull();
        }
    }
}