namespace BankMoneyLaunderer.MoneyLaundryStrategy
{
    public interface IMoneyLaundryConfig
    {
        public decimal MaximumSingelTransactionAmount { get; set; }

        public decimal MaximumXHoursTotalAmount { get; set; }

        public int XHours { get; set; }
    }
}