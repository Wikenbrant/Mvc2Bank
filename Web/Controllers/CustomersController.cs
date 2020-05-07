using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Customers.Queries.GetCustomer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CustomersController : Controller
    {

        public  IActionResult Index([FromRoute] GetCustomerQuery query)
        {
            return ViewComponent( nameof(ViewComponents.CustomerDetail), new { query });
        }
    }
}