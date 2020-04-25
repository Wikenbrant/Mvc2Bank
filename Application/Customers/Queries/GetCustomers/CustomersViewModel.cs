using System.Collections.Generic;

namespace Application.Customers.Queries.GetCustomers
{
    public class CustomersViewModel
    {
        public IEnumerable<CustomersDto> Customers { get; set; }
    }
}