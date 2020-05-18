using System.Threading.Tasks;
using Application.Customers.Queries.GetTop10CustomersByCountry;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents.GetTop10InCountry
{
    public class GetTop10InCountry : ViewComponent
    {
        private readonly IMediator _mediator;

        public GetTop10InCountry(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync(GetTop10CustomersByCountryQuery query)
        {
            var model = await _mediator.Send(query).ConfigureAwait(false);
            return View(model);
        }
    }
}