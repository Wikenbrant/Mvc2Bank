using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Accounts.Queries.GetAccountsByCustomerId
{
    public class GetAccountsByCustomerIdQuery : IRequest<AccountsViewModel>
    {
        public int Id { get; set; }
    }

    public class GetAccountsByCustomerIdQueryHandler : IRequestHandler<GetAccountsByCustomerIdQuery, AccountsViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAccountsByCustomerIdQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AccountsViewModel> Handle(GetAccountsByCustomerIdQuery request, CancellationToken cancellationToken)=> new AccountsViewModel
        {
            Accounts = await _context.Customers
                .Where(c=>c.CustomerId == request.Id)
                .SelectMany(c=>c.Dispositions.Select(d=>d.Account))
                .ProjectTo<AccountsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false)
        };
    }
}