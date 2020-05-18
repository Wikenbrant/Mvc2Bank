using System.Threading.Tasks;
using Application.Statistics.Query.GetNumberOfAccountsAndTotalSumForEachCountry;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents.StatisticsCountries
{
    public class StatisticsCountries : ViewComponent
    {
        private readonly IMediator _mediator;

        public StatisticsCountries(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _mediator.Send(new GetNumberOfAccountsAndTotalSumForEachCountryQuery()).ConfigureAwait(false);
            return View(model);
        }
    }
}