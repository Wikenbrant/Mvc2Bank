using System.Threading.Tasks;
using Application.Customers.Queries.GetCustomersPagination;
using Application.Search.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents.CustomerTableBody
{
    public class CustomerTableBody : ViewComponent
    {
        private readonly IMediator _mediator;

        public CustomerTableBody(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync(SearchQuery query)
        {
            var model = await _mediator.Send(query).ConfigureAwait(false);
            return View(model);
        }
    }
}