using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetCustomers
{
    public class GetCustomersQuery : IRequest<CustomersViewModel>
    {
    }

    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, CustomersViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CustomersViewModel> Handle(GetCustomersQuery request, CancellationToken cancellationToken)=> new CustomersViewModel
        {
            Customers = await _context.Customers
                .ProjectTo<CustomersDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false)
        };
    }
}