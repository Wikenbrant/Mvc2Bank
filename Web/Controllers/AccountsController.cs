using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Accounts.Queries.GetAccount;
using Application.Accounts.Queries.GetAccountsPagination;
using Application.Transactions.Queries.GetTransactionsPagination;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AccountsController : ApiController
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<TransactionsPaginationViewModel>> Index([FromQuery]GetTransactionsPaginationQuery query) =>
            await _mediator.Send(query).ConfigureAwait(false);

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountViewModel>> Get([FromRoute]GetAccountQuery query) =>
            await _mediator.Send(query).ConfigureAwait(false);
    }
}