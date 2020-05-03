using System.Collections.Generic;

namespace BankMoneyLaunderer.Models
{
    public class AccountReport
    {
        public int AccountId { get; set; }

        public IEnumerable<TransactionData> SuspiciousTransactions { get; set; }
    }
}