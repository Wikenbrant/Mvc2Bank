using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ITransactionService
    {
        Task<(Result result, Transaction transaction)> MakeWithdrawalAsync(int accountId, decimal amount,
            string operation, string type, string symbol, string bank, string toAccount, CancellationToken cancellationToken);

        Task<(Result result, Transaction transaction)> MakeInsertAsync(int accountId, decimal amount,
            string operation, string type, string symbol, string bank, string toAccount, CancellationToken cancellationToken);
    }
}