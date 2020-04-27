using System.Threading.Tasks;
using Application.Statistics.Query.GetNumberOfAccountsCustomerAndTotalBalance;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Views.Components.ApplicationStatistics
{
    public class ApplicationStatistics : ViewComponent
    {
        private readonly IMediator _mediator;

        public ApplicationStatistics(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _mediator.Send(new GetNumberOfAccountsCustomerAndTotalBalanceQuery())
                .ConfigureAwait(false);
            return View(model);
        }
    }
}