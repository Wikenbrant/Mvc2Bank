using System.Collections.Generic;

namespace Application.Accounts.Queries.GetAccounts
{
    public class AccountsViewModel
    {
        public IEnumerable<AccountsDto> Accounts { get; set; }
    }
}