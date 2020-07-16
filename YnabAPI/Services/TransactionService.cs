using System.Collections.Generic;

using YnabAPI.Models;

namespace YnabAPI.Services
{
    public class TransactionService
    {
        public IEnumerable<Transaction> GetTransactions(string budgetId)
        {
            return new List<Transaction>();
        }
    }
}
