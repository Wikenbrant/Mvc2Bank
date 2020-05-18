using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Transactions.Commands.CreateTransaction;
using Application.Transactions.Queries.GetTransactionsByAccountIdPagination;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Transactions
        public IActionResult GetTransactionsByAccountId(GetTransactionsByAccountIdPaginationQuery query)
        {
            return ViewComponent(nameof(ViewComponents.CustomerTransactions), new {query});
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateTransaction(CreateTransactionViewModel model)
        {
            if (!ModelState.IsValid) return Json(ModelState.Select(o => o.Value).SelectMany(e=>e.Errors.Select(error => error.ErrorMessage)));

            var command = new CreateTransactionCommand
            {
                AccountId = (int)model.AccountId,
                Amount = model.Amount,
                Account = model.Account.ToString(),
                Type = Enum.Parse<TransactionType>(model.Type,true),
                Symbol = model.Symbol,
                Bank = model.Bank,
                Operation = model.Operation
            };

            var (result, transactionID) = await _mediator.Send(command).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return Json(result.Errors);
        }


        private static CreateTransactionViewModel CreateModel(IEnumerable<int> accountIds) =>
            new CreateTransactionViewModel
            {
                AccountIds = accountIds
            };
            
    }
}