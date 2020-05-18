using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Statistics.Query.GetNumberOfAccountsCustomerAndTotalBalance
{
    public class GetNumberOfAccountsCustomerAndTotalBalanceQuery : IRequest<NumberOfAccountsCustomerAndTotalBalanceViewModel>
    {
    }

    public class GetNumberOfAccountsCustomerAndTotalBalanceQueryHandler : IRequestHandler<
        GetNumberOfAccountsCustomerAndTotalBalanceQuery,NumberOfAccountsCustomerAndTotalBalanceViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetNumberOfAccountsCustomerAndTotalBalanceQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<NumberOfAccountsCustomerAndTotalBalanceViewModel> Handle(
            GetNumberOfAccountsCustomerAndTotalBalanceQuery request, CancellationToken cancellationToken) =>
            new NumberOfAccountsCustomerAndTotalBalanceViewModel
            {
                NumberOfCustomers = await _context.Customers.CountAsync(cancellationToken).ConfigureAwait(false),
                NumberOfAccounts = await _context.Accounts.CountAsync(cancellationToken).ConfigureAwait(false),
                TotalBalance = await _context.Accounts.SumAsync(a=>a.Balance,cancellationToken).ConfigureAwait(false)
            };
    }
}