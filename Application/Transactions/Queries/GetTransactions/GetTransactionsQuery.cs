using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetTransactions
{
    public class GetTransactionsByAccountIdQuery : IRequest<TransactionsViewModel>
    {
    }

    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsByAccountIdQuery, TransactionsViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTransactionsQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TransactionsViewModel> Handle(GetTransactionsByAccountIdQuery request, CancellationToken cancellationToken)=> new TransactionsViewModel
        {
            Transactions = await _context.Transactions
                .ProjectTo<TransactionsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false)
        };
    }
}