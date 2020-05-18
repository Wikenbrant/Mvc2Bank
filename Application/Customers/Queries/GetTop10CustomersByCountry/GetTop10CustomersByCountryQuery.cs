using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetTop10CustomersByCountry
{
    public class GetTop10CustomersByCountryQuery : IRequest<Top10CustomersByCountryViewModel>
    {
        public string Country { get; set; }
    }

    public class
        GetTop10CustomersByCountryQueryHandler : IRequestHandler<GetTop10CustomersByCountryQuery,
            Top10CustomersByCountryViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTop10CustomersByCountryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Top10CustomersByCountryViewModel> Handle(GetTop10CustomersByCountryQuery request,
            CancellationToken cancellationToken) => new Top10CustomersByCountryViewModel
        {
            Customers = await _context.Customers
                .Where(customer => customer.Country == request.Country)
                .OrderByDescending(c => c.Dispositions.Sum(d=>d.Account.Balance))
                .ProjectTo<Top10CustomersByCountryDto>(_mapper.ConfigurationProvider)
                .Take(10)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false)
        };
    }
}


