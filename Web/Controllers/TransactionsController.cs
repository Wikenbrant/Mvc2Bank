using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Transactions.Queries.GetTransactionsByAccountIdPagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class TransactionsController : Controller
    {
        // GET: Transactions
        public IActionResult GetTransactionsByAccountId(int id,int currentPage)
        {
            return ViewComponent(
                nameof(ViewComponents.CustomerTransactions),
                new
                {
                    query = new GetTransactionsByAccountIdPaginationQuery
                    {
                        CurrentPage = currentPage,
                        Id = id
                    }
                });
        }
    }
}