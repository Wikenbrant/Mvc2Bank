using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetTransaction
{
    public class GetTransactionQuery : IRequest<TransactionViewModel>
    {
        public int Id { get; set; }
    }

    public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, TransactionViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTransactionQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<TransactionViewModel> Handle(GetTransactionQuery request, CancellationToken cancellationToken) => new TransactionViewModel
        {
            Transaction = _mapper.Map<TransactionDto>(await _context.Transactions.FirstOrDefaultAsync(
                transaction => transaction.TransactionId == request.Id,
                cancellationToken).ConfigureAwait(false))
        };

    }
}
