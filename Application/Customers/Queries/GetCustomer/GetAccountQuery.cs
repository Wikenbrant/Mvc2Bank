using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
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


        public async Task<CustomerViewModel> Handle(GetCustomerQuery request, CancellationToken cancellationToken) => new CustomerViewModel
        {
            Customer = _mapper.Map<CustomerDto>(await _context.Customers.FirstOrDefaultAsync(
                customer => customer.CustomerId == request.Id,
                cancellationToken).ConfigureAwait(false))
        };

    }
}
