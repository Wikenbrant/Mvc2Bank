using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Accounts.Queries.GetAccounts;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Accounts.Queries.GetAccountsPagination
{
    public class GetAccountsPaginationQuery : IRequest<AccountsPaginationViewModel>
    {
        public int Id { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class GetAccountsPaginationQueryHandler : IRequestHandler<GetAccountsPaginationQuery, AccountsPaginationViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAccountsPaginationQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AccountsPaginationViewModel> Handle(GetAccountsPaginationQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.PageSize;
            var skip = (request.CurrentPage - 1) * pageSize;
            var count = await _context.Accounts
                .CountAsync(cancellationToken)
                .ConfigureAwait(false);

            return new AccountsPaginationViewModel
            {
                CurrentPage = request.CurrentPage,
                PageSize = pageSize,
                PageCount = (int)Math.Ceiling((double)count / pageSize),
                Accounts = await _context.Accounts
                    .Skip(skip)
                    .Take(pageSize)
                    .ProjectTo<AccountsDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false)
            };
        }
    }
}