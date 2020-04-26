using Microsoft.Azure.Search;

namespace Domain.Entities
{
    public partial class PermanentOrder
    {
        public int OrderId { get; set; }
        public int AccountId { get; set; }
        public string BankTo { get; set; }
        public string AccountTo { get; set; }
        public decimal? Amount { get; set; }
        public string Symbol { get; set; }

        public virtual Account Account { get; set; }
    }
}
