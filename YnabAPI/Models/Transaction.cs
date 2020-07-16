using System;

namespace YnabAPI.Models
{
    public class Transaction
    {
        public DateTime TransactionDate { get; set; }

        public decimal Amount { get; set; }

        public string Category { get; set; }
    }
}
