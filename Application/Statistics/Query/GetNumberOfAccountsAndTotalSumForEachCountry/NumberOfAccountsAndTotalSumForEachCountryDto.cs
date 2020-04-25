namespace Application.Statistics.Query
{
    public class NumberOfAccountsAndTotalSumForEachCountryDto
    {
        public string Country { get; set; }
        public int NumberOfCustomers { get; set; }

        public decimal Total { get; set; }
    }
}