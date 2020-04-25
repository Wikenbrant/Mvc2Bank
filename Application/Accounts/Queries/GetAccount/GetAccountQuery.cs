using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Accounts.Queries.GetAccount
{
    public class GetAccountQuery : IRequest<AccountDto>
    {
        public int Id { get; set; }
    }

    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, AccountDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAccountQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public Task<AccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken) => _context
            .Accounts.ProjectTo<AccountDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(
                account => account.AccountId == request.Id,
                cancellationToken);

    }
}
