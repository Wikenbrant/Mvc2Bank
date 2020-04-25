using Application.Accounts.Queries.GetAccounts;

namespace Application.Accounts.Queries.GetAccountsPagination
{
    public class AccountsPaginationViewModel : AccountsViewModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}