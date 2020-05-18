using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Customers.Queries.GetCustomers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetCustomersPaginationOrderOn
{
    public class GetCustomersPaginationOrderOnQuery : IRequest<GetCustomersPaginationOrderOnViewModel>
    {
        public int Id { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public OrderByType OrderBy { get; set; }

        public Expression<Func<Customer,object>> OrderOnProperty { get; set; }
    }

    public class GetCustomersPaginationOrderOnQueryHandler : IRequestHandler<GetCustomersPaginationOrderOnQuery, GetCustomersPaginationOrderOnViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersPaginationOrderOnQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetCustomersPaginationOrderOnViewModel> Handle(GetCustomersPaginationOrderOnQuery request, CancellationToken cancellationToken)
        {
            var pageSize = request.PageSize;
            var skip = (request.CurrentPage - 1) * pageSize;
            var count = await _context.Accounts
                .CountAsync(cancellationToken)
                .ConfigureAwait(false);

            var customers = request.OrderBy switch
            {
                OrderByType.Ascending => _context.Customers
                    .Skip(skip)
                    .Take(pageSize)
                    .OrderBy(request.OrderOnProperty)
                    .ProjectTo<CustomersDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken),

                OrderByType.Descending => _context.Customers
                    .Skip(skip)
                    .Take(pageSize)
                    .OrderByDescending(request.OrderOnProperty)
                    .ProjectTo<CustomersDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken),

                _ => _context.Customers
                    .Skip(skip)
                    .Take(pageSize)
                    .ProjectTo<CustomersDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };

            return new GetCustomersPaginationOrderOnViewModel
            {
                CurrentPage = request.CurrentPage,
                PageSize = pageSize,
                PageCount = (int)Math.Ceiling((double)count / pageSize),
                Customers = await customers
                    .ConfigureAwait(false)
            };
        }
    }
}