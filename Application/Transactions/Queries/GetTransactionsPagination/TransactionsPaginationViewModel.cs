using Application.Transactions.Queries.GetTransactions;

namespace Application.Transactions.Queries.GetTransactionsPagination
{
    public class TransactionsPaginationViewModel : TransactionsViewModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}