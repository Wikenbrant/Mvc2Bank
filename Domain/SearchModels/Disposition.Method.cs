using System;
using System.Text;

namespace Domain.SearchModels
{
    partial class DispositionSearch
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (DispositionId > 0)
            {
                sb.Append($"{nameof(DispositionId)}: {DispositionId}\n");
            }

            if (CustomerId > 0)
            {
                sb.Append($"{nameof(CustomerId)}: {CustomerId}\n");
            }

            if (AccountId > 0)
            {
                sb.Append($"{nameof(AccountId)}: {AccountId}\n");
            }

            if (!String.IsNullOrEmpty(Type))
            {
                sb.Append($"{nameof(Type)}: {Type}\n");
            }

            return sb.ToString();
        }
    }
}
