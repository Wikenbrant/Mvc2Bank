using System.Linq;
using System.Threading.Tasks;
using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Queries.GetCustomer;
using Application.Customers.Queries.GetCustomersPagination;
using Application.Search.Query;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.SearchModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CustomersController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerViewModel model)
        {
            if (!ModelState.IsValid) return Json(ModelState.Select(o => o.Value).SelectMany(e => e.Errors.Select(error => error.ErrorMessage)));

            var command = new CreateCustomerCommand
            {
                Customer = _mapper.Map<Customer>(model)
            };

            var (result, customerId) = await _mediator.Send(command).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return Json(new
                {
                    redirect = Url.Action(nameof(Confirmation), new { id = customerId })
                });
            }

            return Json(result.Errors);
        }

        public async Task<IActionResult> Confirmation(int id)
        {
            var model = await _mediator.Send(new GetCustomerQuery { Id = id }).ConfigureAwait(false);
            return View(model);
        }
    }
}