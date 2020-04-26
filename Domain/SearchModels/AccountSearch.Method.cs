using System;
using System.Text;

namespace Domain.SearchModels
{
    partial class AccountSearch
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (AccountId > 0)
            {
                sb.Append($"{nameof(AccountId)}: {AccountId}\n");
            }

            if (!String.IsNullOrEmpty(Frequency))
            {
                sb.Append($"{nameof(Frequency)}: {Frequency}\n");
            }

            sb.Append($"{nameof(Created)}: {Created}\n");

            sb.Append($"{nameof(Balance)}: {Balance}\n");


            return sb.ToString();
        }
    }
}
