using System.Collections.Generic;

namespace Application.Customers.Queries.GetTop10CustomersByCountry
{
    public class Top10CustomersByCountryViewModel
    {
        public IEnumerable<Top10CustomersByCountryDto> Customers { get; set; }
    }
}