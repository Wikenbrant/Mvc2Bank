using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetSumOfTransactions
{
    public class GetSumOfTransactionsQuery : IRequest<decimal>
    {
    }

    public class GetSumOfTransactionsQueryHandler : IRequestHandler<GetSumOfTransactionsQuery, decimal>
    {
        private readonly IApplicationDbContext _context;

        public GetSumOfTransactionsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<decimal> Handle(GetSumOfTransactionsQuery request, CancellationToken cancellationToken) =>
            _context.Transactions.SumAsync(transaction => transaction.Amount, cancellationToken);
    }
}