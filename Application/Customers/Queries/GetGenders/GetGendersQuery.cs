using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetGenders
{
    public class GetGendersQuery : IRequest<IEnumerable<string>>
    {
    }

    public class GetGendersQueryHandler : IRequestHandler<GetGendersQuery, IEnumerable<string>>
    {
        private readonly IApplicationDbContext _context;

        public GetGendersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> Handle(GetGendersQuery request, CancellationToken cancellationToken) => 
            await _context.Customers
                .Select(c=>c.Gender)
                .Distinct()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
    }
}
