using System.Threading.Tasks;
using Application.Transactions.Queries.GetTransactionsByAccountIdPagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents.CustomerTransactions
{
    public class CustomerTransactions : ViewComponent
    {
        private readonly IMediator _mediator;

        public CustomerTransactions(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync(GetTransactionsByAccountIdPaginationQuery query)
        {
            var model = await _mediator.Send(query).ConfigureAwait(false);
            return View(model);
        }
    }
}