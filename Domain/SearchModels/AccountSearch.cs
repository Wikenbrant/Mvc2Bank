using System;
using Microsoft.Azure.Search;

namespace Domain.SearchModels
{
    public partial class AccountSearch
    {
        [IsFilterable]
        public int AccountId { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        public string Frequency { get; set; }

        [ IsFilterable, IsFacetable]
        public DateTime Created { get; set; }

        [ IsFacetable]
        public double Balance { get; set; }
    }
}