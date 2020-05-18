using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.SearchModels;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Search.Repository
{
    public interface IRepository
    {
        public Task<IEnumerable<CustomerSearch>> GetCustomersAsync();
    }

    class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Repository(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerSearch>> GetCustomersAsync() =>
            await _context.Customers
                .ProjectTo<CustomerSearch>(_mapper.ConfigurationProvider)
                .ToListAsync()
                .ConfigureAwait(false);
    }
}