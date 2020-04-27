using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Transactions.Queries.GetTransactions;
using Application.Transactions.Queries.GetTransactionsPagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetTransactionsByAccountIdPagination
{
    public class GetTransactionsByAccountIdPaginationQuery : IRequest<TransactionsPaginationViewModel>
    {
        public int Id { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class GetAccountsPaginationQueryHandler : IRequestHandler<GetTransactionsByAccountIdPaginationQuery, TransactionsPaginationViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAccountsPaginationQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TransactionsPaginationViewModel> Handle(GetTransactionsByAccountIdPaginationQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.PageSize;
            var skip = (request.CurrentPage - 1) * pageSize;
            var count = await _context.Accounts
                .CountAsync(cancellationToken)
                .ConfigureAwait(false);

            return new TransactionsPaginationViewModel
            {
                CurrentPage = request.CurrentPage,
                PageSize = pageSize,
                PageCount = (int) Math.Ceiling((double) count / pageSize),
                Transactions = await _context.Transactions
                    .Where(transaction => transaction.AccountId == request.Id)
                    .Skip(skip)
                    .Take(pageSize)
                    .ProjectTo<TransactionsDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false)
            };
        }
    }
}