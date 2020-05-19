using Application.Customers.Queries.GetCustomer;
using Application.Customers.Queries.GetCustomersPagination;
using Application.Search.Query;
using Domain.Enums;
using Domain.SearchModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        public IActionResult Index([FromRoute] GetCustomerQuery query)
        {
            return ViewComponent(nameof(ViewComponents.CustomerDetail), new {query});
        }

        public IActionResult GetCustomers(int page = 1, string searchText = "" , string orderByField = "", OrderByType orderBy = OrderByType.None)
        {
            return ViewComponent(nameof(ViewComponents.CustomerTable),
                new
                {
                    query = new SearchQuery
                    {
                        Page = page, 
                        Search = new SearchData
                        {
                            SearchText = searchText
                        },
                        OrderByField = orderByField,
                        OrderByType = orderBy
                    }
                });
        }
    }
}