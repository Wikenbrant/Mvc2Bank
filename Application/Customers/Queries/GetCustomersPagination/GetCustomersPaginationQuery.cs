using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Customers.Queries.GetCustomers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetCustomersPagination
{
    public class GetCustomersPaginationQuery : IRequest<CustomersPaginationViewModel>
    {
        public int Id { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class GetCustomersPaginationQueryHandler : IRequestHandler<GetCustomersPaginationQuery, CustomersPaginationViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersPaginationQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomersPaginationViewModel> Handle(GetCustomersPaginationQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.PageSize;
            var skip = (request.CurrentPage - 1) * pageSize;
            var count = await _context.Accounts
                .CountAsync(cancellationToken)
                .ConfigureAwait(false);

            return new CustomersPaginationViewModel
            {
                CurrentPage = request.CurrentPage,
                PageSize = pageSize,
                PageCount = (int)Math.Ceiling((double)count / pageSize),
                Customers = await _context.Customers
                    .Skip(skip)
                    .Take(pageSize)
                    .ProjectTo<CustomersDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false)
            };
        }
    }
}