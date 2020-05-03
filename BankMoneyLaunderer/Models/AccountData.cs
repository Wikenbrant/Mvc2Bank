using System.Collections.Generic;

namespace BankMoneyLaunderer.Models
{
    public class AccountData
    {
        public int AccountId { get; set; }

        public IEnumerable<TransactionData> Transactions { get; set; }
    }
}