using System.Threading.Tasks;
using Application.Accounts.Queries.GetAccount;
using Application.Transactions.Queries.GetTransactionsPagination;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountsController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController(IMediator mediator,UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<TransactionsPaginationViewModel>> Index([FromQuery]GetTransactionsPaginationQuery query) =>
            await _mediator.Send(query).ConfigureAwait(false);

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountViewModel>> Get([FromRoute]GetAccountQuery query) =>
            await _mediator.Send(query).ConfigureAwait(false);

        [HttpGet("/me")]
        public async Task<ActionResult<TransactionsPaginationViewModel>> Me() =>
            Ok(await _userManager.GetUserAsync(User).ConfigureAwait(false));
    }
}