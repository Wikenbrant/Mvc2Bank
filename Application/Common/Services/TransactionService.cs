using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Services
{
    class TransactionService : ITransactionService
    {
        private readonly IApplicationDbContext _context;

        public TransactionService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<(Result result, Transaction transaction)> MakeTransactionAsync(
            int accountId, decimal amount, string operation, string type,string symbol,string bank,string toAccount, 
            CancellationToken cancellationToken)
        {
            var account = await _context.Accounts.FindAsync(accountId, cancellationToken).ConfigureAwait(false);

            if (account == null)
            {
                return (Result.Failure(new[] { "Account does not exist" }), null);
            }

            if (account.Balance < amount)
            {
                return (Result.Failure(new[] { "Balance is to low" }), null);
            }

            account.Balance -= amount;

            var entity = new Transaction
            {
                AccountId = accountId,
                AccountNavigation = account,
                Date = DateTime.Today,
                Type = type,
                Operation = operation,
                Amount = amount,
                Balance = account.Balance,
                Symbol = symbol,
                Bank = bank,
                Account = toAccount
            };

            _context.Transactions.Add(entity);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return (Result.Success(), entity);
        }
    }
}