using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetCustomer
{
    public class GetCustomerQuery : IRequest<CustomerViewModel>
    {
        public int Id { get; set; }
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<CustomerViewModel> Handle(GetCustomerQuery request, CancellationToken cancellationToken) =>
            new CustomerViewModel
            {
                Customer = await _context.Customers
                    .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                    .Where(c => c.CustomerId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false)
            };

    }
}
