using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Statistics.Query.GetNumberOfAccountsAndTotalSumForEachCountry
{
    public class GetNumberOfAccountsAndTotalSumForEachCountryQuery : IRequest<NumberOfAccountsAndTotalSumForEachCountryViewModel>
    {
    }

    public class GetNumberOfAccountsAndTotalSumForEachCountryQueryHandler : IRequestHandler<
        GetNumberOfAccountsAndTotalSumForEachCountryQuery, NumberOfAccountsAndTotalSumForEachCountryViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetNumberOfAccountsAndTotalSumForEachCountryQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<NumberOfAccountsAndTotalSumForEachCountryViewModel> Handle(GetNumberOfAccountsAndTotalSumForEachCountryQuery request, CancellationToken cancellationToken) => new NumberOfAccountsAndTotalSumForEachCountryViewModel
        {
            Countries = await _context.Customers
                .SelectMany(customer => _context.Dispositions
                    .Where(d => d.CustomerId == customer.CustomerId)
                    .SelectMany(disposition => _context.Accounts
                        .Where(a => disposition.AccountId == a.AccountId)
                        .Select(account => new
                        {
                            customer.CustomerId,
                            customer.Country,
                            account.Balance
                        })
                    )
                )
                .GroupBy(g => new
                {
                    g.Country
                })
                .Select(g => new NumberOfAccountsAndTotalSumForEachCountryDto
                {
                    Country = g.Key.Country,
                    NumberOfCustomers = g.Count(),
                    Total = g.Sum(b => b.Balance)
                })
                .ToListAsync(cancellationToken).ConfigureAwait(false)

            //Motsvarar

            //SELECT
            //c.Country AS Country,
            //COUNT(c.CustomerId) AS NumberOfCustomers,
            //Sum(a.Balance) AS Total
            //FROM[BankAppData].[dbo].[Customers] AS c
            //INNER JOIN[BankAppData].[dbo].[Dispositions] AS d ON c.CustomerId = d.CustomerId
            //INNER JOIN Accounts AS a ON d.AccountId = a.AccountId
            //GROUP BY c.Country

        };
    }
}