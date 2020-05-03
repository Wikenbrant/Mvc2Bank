using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Transactions.Commands.CreateTransaction;
using Application.Transactions.Queries.GetTransactionsByAccountIdPagination;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult CreateTransaction(int id)
        {
            var x = OperationTypes.Operations.ToList();
            var model = CreateModel(id);
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateTransaction(CreateTransactionCommand command)
        {
            if (!ModelState.IsValid) return View(command);
            var (result, transactionID) = await _mediator.Send(command).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(command);
        }


        private static CreateTransactionViewModel CreateModel(int accountId)=>
        new CreateTransactionViewModel
        {
            AccountId = accountId,
            Types = Enum.GetValues(typeof(TransactionType)).Cast<TransactionType>().Select(t =>
                new SelectListItem
                {
                    Text = t.ToString(),
                    Value = t.ToString()
                }).ToList(),
            Operations = OperationTypes.Operations.Select(o =>
                new SelectListItem
                {
                    Text = o.ToString(),
                    Value = o.ToString()
                }).ToList(),
            Symbols = SymbolType.Symbols.Select(s =>
                new SelectListItem
                {
                    Text = s.ToString(),
                    Value = s.ToString()
                }).ToList()
        };
    }
}