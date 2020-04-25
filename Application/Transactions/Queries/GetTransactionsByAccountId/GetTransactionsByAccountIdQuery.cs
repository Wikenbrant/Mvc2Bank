using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Transactions.Queries.GetTransactions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetTransactionsByAccountId
{
    public class GetTransactionsByAccountIdQuery : IRequest<TransactionsViewModel>
    {
        public int Id { get; set; }
    }

    public class GetTransactionsByAccountIdQueryHandler : IRequestHandler<GetTransactionsByAccountIdQuery, TransactionsViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTransactionsByAccountIdQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TransactionsViewModel> Handle(GetTransactionsByAccountIdQuery request,
            CancellationToken cancellationToken) => new TransactionsViewModel
        {
            Transactions = await _context.Transactions
                .Where(transaction => transaction.AccountId == request.Id)
                .ProjectTo<TransactionsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false)
        };
    }
}