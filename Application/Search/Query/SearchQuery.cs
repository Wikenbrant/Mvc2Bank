using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Domain.SearchModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Search.Query
{
    public class SearchQuery : IRequest<SearchViewModel>
    {
        public SearchData Search { get; set; }

        public int Page { get; set; } = 1;

        public string[] SearchFields { get; set; } = {nameof(CustomerSearch.Givenname),nameof(CustomerSearch.Surname),nameof(CustomerSearch.City)};

        public string[] SelectFields { get; set; } = { nameof(CustomerSearch.CustomerId) };

        public string OrderByField { get; set; }
        public OrderByType OrderByType { get; set; }
    }

    public class SearchQueryHandler : IRequestHandler<SearchQuery, SearchViewModel>
    {
        private readonly ISearchService _searchService;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchQueryHandler(ISearchService searchService,IApplicationDbContext context, IMapper mapper)
        {
            _searchService = searchService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<SearchViewModel> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            var search = await _searchService.RunQueryAsync(
                request.Search,
                request.Page,
                0,
                request.SearchFields,
                (request.OrderByType, request.OrderByField) switch
                {
                    (OrderByType.Ascending, string field) => new[] { field },
                    (OrderByType.Descending, string field) => new[] { $"{field} desc" },
                    _ => null
                },
                request.SelectFields).ConfigureAwait(false);

            var ids = search.ResultList.Results.Select(d => Convert.ToInt32(d.Document.CustomerId)).ToArray();

            var customers = await _context.Customers
                .Where(c => ids.Contains(c.CustomerId))
                .ProjectTo<SearchDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new SearchViewModel
            {
                Customers = customers,
                CurrentPage = search.CurrentPage,
                PageCount = search.PageCount,
                SearchText = search.SearchText,
                PageRange = search.PageRange,
                OrderByField = request.OrderByField,
                OrderByType = request.OrderByType
            };
        }
            
    }
}