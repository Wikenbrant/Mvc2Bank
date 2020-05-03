using System.Threading.Tasks;
using Application.Customers.Queries.GetCustomer;
using Application.Statistics.Query.GetNumberOfAccountsCustomerAndTotalBalance;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents.CustomerDetail
{
    public class CustomerDetail : ViewComponent
    {
        private readonly IMediator _mediator;

        public CustomerDetail(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync(GetCustomerQuery query)
        {
            var model = await _mediator.Send(query)
                .ConfigureAwait(false);
            return View(model);
        }
    }
}