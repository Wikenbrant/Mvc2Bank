using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Customers.Queries.GetCountries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetCountryCodes
{
    public class GetCountryCodesQuery : IRequest<IEnumerable<string>>
    {
    }

    public class GetCountryCodesQueryHandler : IRequestHandler<GetCountryCodesQuery, IEnumerable<string>>
    {
        private readonly IApplicationDbContext _context;

        public GetCountryCodesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> Handle(GetCountryCodesQuery request, CancellationToken cancellationToken) =>
            await _context.Customers
                .Select(c => c.CountryCode)
                .Distinct()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
    }
}
