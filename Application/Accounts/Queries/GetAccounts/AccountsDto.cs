using System;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Accounts.Queries.GetAccounts
{
    public class AccountsDto : IMapFrom<Account>
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
    }
}
