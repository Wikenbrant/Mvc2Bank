using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Accounts.Queries.GetNumberOfAccounts
{
    public class GetNumberOfAccountsQuery : IRequest<int>
    {
    }

    public class GetNumberOfAccountsQueryHandler : IRequestHandler<GetNumberOfAccountsQuery, int> 
    {
        private readonly IApplicationDbContext _context;

        public GetNumberOfAccountsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> Handle(GetNumberOfAccountsQuery request, CancellationToken cancellationToken) =>
            _context.Accounts.CountAsync(cancellationToken);
    }
}