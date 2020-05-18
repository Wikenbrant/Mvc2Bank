using System.Collections.Generic;

namespace Application.Accounts.Queries.GetAccountsByCustomerId
{
    public class AccountsViewModel
    {
        public IEnumerable<AccountsDto> Accounts { get; set; }
    }
}