using System.Threading.Tasks;
using Application.Search.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents.CustomerTable
{
    public class CustomerTable : ViewComponent
    {
        private readonly IMediator _mediator;

        public CustomerTable(IMediator mediator)
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