using System.Threading.Tasks;
using Application.Accounts.Commands.CreateAccount;
using Application.Transactions.Commands.CreateTransaction;
using Application.Transactions.Queries.GetTransactions;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.Test.Transactions.Commands
{
    using static Testing;
    public class CreateTransactionTest : TestBase
    {
        [Test]
        public async Task ShouldCreateTransaction()
        {
            var accountId = await SendAsync(new CreateAccountCommand());

            var (result, transactionId) = await SendAsync(new CreateTransactionCommand
            {
                AccountId = accountId ,
                Amount = 200, 
                Type = TransactionType.Credit
            });

            result.Succeeded.Should().BeTrue();
            transactionId.Should().NotBeNull();

            var model = await SendAsync(new GetTransactionsByAccountIdQuery{Id = accountId});

            model.Transactions.Should().HaveCount(1);
        }
        [Test]
        public async Task ShouldFailCreateTransaction()
        {
            var accountId = await SendAsync(new CreateAccountCommand());

            var (result, transactionId) = await SendAsync(new CreateTransactionCommand
            {
                AccountId = accountId, 
                Amount = 200, 
                Type = TransactionType.Debit
            });

            result.Succeeded.Should().BeFalse();
            transactionId.Should().BeNull();

            var model = await SendAsync(new GetTransactionsByAccountIdQuery { Id = accountId });

            model.Transactions.Should().HaveCount(0);
        }

        [Test]
        public async Task ShouldInsertThenWithdraw()
        {
            var accountId = await SendAsync(new CreateAccountCommand());

            var (insertResult, insertTransactionId) = await SendAsync(new CreateTransactionCommand
            {
                AccountId = accountId, 
                Amount = 200, 
                Type = TransactionType.Credit
            });

            insertResult.Succeeded.Should().BeTrue();
            insertTransactionId.Should().NotBeNull();

            var (withdrawResult, withdrawTransactionId) = await SendAsync(new CreateTransactionCommand
            {
                AccountId = accountId, 
                Amount = 200, 
                Type = TransactionType.Debit
            });

            withdrawResult.Succeeded.Should().BeTrue();
            withdrawTransactionId.Should().NotBeNull();

            var model = await SendAsync(new GetTransactionsByAccountIdQuery { Id = accountId });

            model.Transactions.Should().HaveCount(2);
        }

        [Test]
        public async Task ShouldFailInsertThenWithdraw()
        {
            var accountId = await SendAsync(new CreateAccountCommand());

            var (insertResult, insertTransactionId) = await SendAsync(new CreateTransactionCommand
            {
                AccountId = accountId, 
                Amount = 200, 
                Type = TransactionType.Credit
            });

            insertResult.Succeeded.Should().BeTrue();
            insertTransactionId.Should().NotBeNull();

            var (withdrawResult, withdrawTransactionId) = await SendAsync(new CreateTransactionCommand
            {
                AccountId = accountId, 
                Amount = 300, 
                Type = TransactionType.Debit
            });

            withdrawResult.Succeeded.Should().BeFalse();
            withdrawTransactionId.Should().BeNull();

            var model = await SendAsync(new GetTransactionsByAccountIdQuery { Id = accountId });

            model.Transactions.Should().HaveCount(1);
        }
    }
}