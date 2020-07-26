using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using YnabAPI.ExternalModels;
using YnabAPI.Models;

namespace YnabAPI.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactions(string budgetId, string? startDate);
    }

    public class TransactionService : ITransactionService
    {
        private readonly IYnabService ynabService;

        public TransactionService(IYnabService ynabService)
        {
            this.ynabService = ynabService;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(string budgetId, string? startDate)
        {
            YnabResponse? result = await this.ynabService.GetTransactions(budgetId, startDate);
            var ynabTransactions = result.Data.Transactions;

            IEnumerable<Transaction> transactions = this.GetSingleTransactions(ynabTransactions)
                .Concat(this.GetSplitTransactions(ynabTransactions));

            return transactions;
        }

        private IEnumerable<Transaction> GetSingleTransactions(IEnumerable<YnabTransaction> ynabTransactions) 
            => ynabTransactions.Where(x => x.Approved && !x.SubTransactions.Any()).Select(x => new Transaction(x.Date, x.Amount / 1000, x.CategoryName));

        private IEnumerable<Transaction> GetSplitTransactions(IEnumerable<YnabTransaction> ynabTransactions)
        {
            var transactions = new List<Transaction>();

            var splitTransactions = ynabTransactions.Where(x => x.Approved && x.SubTransactions.Any());

            foreach (var splitTransaction in splitTransactions)
            {
                transactions.AddRange(splitTransaction.SubTransactions.Select(x => new Transaction(x.Date, x.Amount, x.CategoryName)));
            }

            return transactions;
        }
    }
}
