using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Enums;
using Domain.SearchModels;
using MediatR;

namespace Application.Search.Query
{
    public class SearchQuery : IRequest<SearchData>
    {
        public SearchData Search { get; set; }

        public int Page { get; set; } = 1;

        public string[] SearchFields { get; set; } = {nameof(CustomerSearch.Givenname),nameof(CustomerSearch.Surname),nameof(CustomerSearch.City)};

        public string OrderByField { get; set; }
        public OrderByType OrderByType { get; set; }
    }

    public class SearchQueryHandler : IRequestHandler<SearchQuery, SearchData>
    {
        private readonly ISearchService _searchService;

        public SearchQueryHandler(ISearchService searchService)
        {
            _searchService = searchService;
        }
        public Task<SearchData> Handle(SearchQuery request, CancellationToken cancellationToken) => 
            _searchService.RunQueryAsync(
                request.Search, 
                request.Page,
                0,
                request.SearchFields,
                (request.OrderByType,request.OrderByField) switch {
                    (OrderByType.Ascending, string field)=> new []{ field },
                    (OrderByType.Descending, string field) => new[] { $"{field} desc" },
                    _ => null
                });
    }
}