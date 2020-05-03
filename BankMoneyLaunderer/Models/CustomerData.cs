using System.Collections.Generic;

namespace BankMoneyLaunderer.Models
{
    public class CustomerData
    {
        public int CustomerId { get; set; }

        public string Givenname { get; set; }
        public string Surname { get; set; }

        public IEnumerable<AccountData> Accounts { get; set; }
    }
}