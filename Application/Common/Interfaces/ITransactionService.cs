using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ITransactionService
    {
        Task<(Result result, Transaction transaction)> MakeWithdrawalAsync(int accountId, decimal amount,
            string operation, string symbol, string bank, string toAccount, CancellationToken cancellationToken);

        Task<(Result result, Transaction transaction)> MakeDepositAsync(int accountId, decimal amount,
            string operation, string symbol, string bank, string toAccount, CancellationToken cancellationToken);
    }
}