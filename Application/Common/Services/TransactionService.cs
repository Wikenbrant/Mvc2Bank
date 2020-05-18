using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Services
{
    class TransactionService : ITransactionService
    {
        private readonly IApplicationDbContext _context;

        public TransactionService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<(Result result, Transaction transaction)> MakeWithdrawalAsync(
            int accountId, decimal amount, string operation,string symbol,string bank,string toAccount, 
            CancellationToken cancellationToken)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId, cancellationToken)
                .ConfigureAwait(false);

            if (account == null)
            {
                return (Result.Failure(new[] { "Account does not exist" }), null);
            }

            if (amount < 0)
            {
                return (Result.Failure(new[] { "Amount is negative" }), null);
            }

            if (account.Balance - amount < 0)
            {
                return (Result.Failure(new[] { "Balance is to low" }), null);
            }

            account.Balance -= amount;

            var entity = new Transaction
            {
                AccountId = accountId,
                AccountNavigation = account,
                Date = DateTime.Today,
                Type = TransactionType.Debit.ToString(),
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

        public async Task<(Result result, Transaction transaction)> MakeDepositAsync(int accountId, decimal amount, string operation, string symbol, string bank,
            string toAccount, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId, cancellationToken)
                .ConfigureAwait(false);

            if (account == null)
            {
                return (Result.Failure(new[] { "Account does not exist" }), null);
            }

            if (amount < 0)
            {
                return (Result.Failure(new[] { "Amount is negative" }), null);
            }

            account.Balance += amount;

            var entity = new Transaction
            {
                AccountId = accountId,
                AccountNavigation = account,
                Date = DateTime.Today,
                Type = TransactionType.Credit.ToString(),
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