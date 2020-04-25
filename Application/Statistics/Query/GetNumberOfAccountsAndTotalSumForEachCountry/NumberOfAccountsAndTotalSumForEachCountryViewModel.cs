using System.Collections.Generic;

namespace Application.Statistics.Query.GetNumberOfAccountsAndTotalSumForEachCountry
{
    public class NumberOfAccountsAndTotalSumForEachCountryViewModel
    {
        public IEnumerable<NumberOfAccountsAndTotalSumForEachCountryDto> Countries { get; set; }
    }
}