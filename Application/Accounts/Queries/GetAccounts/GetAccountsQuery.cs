using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Accounts.Queries.GetAccounts
{
    public class GetAccountsQuery : IRequest<AccountsViewModel>
    {
    }

    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, AccountsViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAccountsQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AccountsViewModel> Handle(GetAccountsQuery request, CancellationToken cancellationToken)=> new AccountsViewModel
        {
            Accounts = await _context.Accounts
                .ProjectTo<AccountsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false)
        };
    }
}