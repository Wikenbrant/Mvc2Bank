using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetNumberOfCustomers
{
    public class GetNumberOfCustomersQuery : IRequest<int>
    {
    }

    public class GetNumberOfCustomersQueryHandler : IRequestHandler<GetNumberOfCustomersQuery, int> 
    {
        private readonly IApplicationDbContext _context;

        public GetNumberOfCustomersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> Handle(GetNumberOfCustomersQuery request, CancellationToken cancellationToken) =>
            _context.Customers.CountAsync(cancellationToken);
    }
}