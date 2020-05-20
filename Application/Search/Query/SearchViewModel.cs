using System;
using System.Collections.Generic;
using System.Text;
using Domain.Enums;

namespace Application.Search.Query
{
    public class SearchViewModel
    {
        public IEnumerable<SearchDto> Customers { get; set; }

        public int CurrentPage { get; set; }

        public int PageRange { get; set; }

        public int PageCount { get; set; }

        public string SearchText { get; set; }

        public OrderByType OrderByType { get; set; }

        public string OrderByField { get; set; }
    }
}
