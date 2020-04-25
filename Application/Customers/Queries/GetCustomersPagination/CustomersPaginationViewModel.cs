using Application.Customers.Queries.GetCustomers;

namespace Application.Customers.Queries.GetCustomersPagination
{
    public class CustomersPaginationViewModel : CustomersViewModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}