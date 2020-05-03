using System.Collections.Generic;

namespace BankMoneyLaunderer.Models
{
    public class CustomerReport
    {
        public int CustomerId { get; set; }

        public string Givenname { get; set; }
        public string Surname { get; set; }

        public IEnumerable<AccountReport> SuspiciousAccounts { get; set; }
    }
}