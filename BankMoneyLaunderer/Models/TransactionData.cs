using System;

namespace BankMoneyLaunderer.Models
{
    public class TransactionData
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}