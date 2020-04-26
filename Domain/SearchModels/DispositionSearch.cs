using Microsoft.Azure.Search;

namespace Domain.SearchModels
{
    public partial class DispositionSearch
    {
        [IsFilterable]
        public int DispositionId { get; set; }

        [IsFilterable]
        public int CustomerId { get; set; }

        [IsFilterable]
        public int AccountId { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        public string Type { get; set; }

        public virtual AccountSearch Account { get; set; }
    }
}