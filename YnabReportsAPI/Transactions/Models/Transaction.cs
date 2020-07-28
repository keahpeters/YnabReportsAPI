using System;

namespace YnabReportsAPI.Transactions.Models
{
    public class Transaction
    {
        public Transaction(DateTime transactionDate, decimal amount, string category)
        {
            this.TransactionDate = transactionDate;
            this.Amount = amount;
            this.Category = category;
        }

        public DateTime TransactionDate { get; }

        public decimal Amount { get; }

        public string Category { get; }
    }
}
