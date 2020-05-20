using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetCountries
{
    public class GetCountriesQuery : IRequest<IEnumerable<string>>
    {
    }

    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IEnumerable<string>>
    {
        private readonly IApplicationDbContext _context;

        public GetCountriesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> Handle(GetCountriesQuery request, CancellationToken cancellationToken) =>
            await _context.Customers
                .Select(c => c.Country)
                .Distinct()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
    }
}
