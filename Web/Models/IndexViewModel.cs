using Application.Customers.Queries.GetCustomersPagination;
using Application.Statistics.Query.GetNumberOfAccountsAndTotalSumForEachCountry;

namespace Web.Models
{
    public class IndexViewModel
    {
        public NumberOfAccountsAndTotalSumForEachCountryViewModel Countries { get; set; }

        public CreateCustomerViewModel CreateCustomerViewModel { get; set; }
    }
}
