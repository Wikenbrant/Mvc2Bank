using System.Collections.Generic;

namespace Application.Transactions.Queries.GetTransactions
{
    public class TransactionsViewModel
    {
        public IEnumerable<TransactionsDto> Transactions { get; set; }
    }
}