using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YnabReportsAPI.Transactions.Models;
using YnabReportsAPI.YnabAPI.Exceptions;
using YnabReportsAPI.YnabAPI.ExternalModels;
using YnabReportsAPI.YnabAPI.Services;

namespace YnabReportsAPI.Transactions.Services
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
            YnabResponse result = await this.ynabService.GetTransactions(budgetId, startDate);

            if (result?.Data?.Transactions is null)
            {
                throw new YnabResponseException("The response from the YNAB API does not contain any transactions");
            }

            IEnumerable<YnabTransaction> ynabTransactions = result.Data.Transactions;

            IEnumerable<Transaction> transactions = this.GetSingleTransactions(ynabTransactions)
                .Concat(this.GetSplitTransactions(ynabTransactions));

            return transactions;
        }

        private IEnumerable<Transaction> GetSingleTransactions(IEnumerable<YnabTransaction> ynabTransactions)
            => ynabTransactions.Where(x => x.Approved && !x.SubTransactions.Any()).Select(x => new Transaction(x.Date, this.ConvertMilliUnitsToDecimal(x.Amount), x.CategoryName));

        private IEnumerable<Transaction> GetSplitTransactions(IEnumerable<YnabTransaction> ynabTransactions)
        {
            var transactions = new List<Transaction>();

            var splitTransactions = ynabTransactions.Where(x => x.Approved && x.SubTransactions.Any());

            foreach (var splitTransaction in splitTransactions)
            {
                transactions.AddRange(splitTransaction.SubTransactions.Select(x => new Transaction(x.Date, this.ConvertMilliUnitsToDecimal(x.Amount), x.CategoryName)));
            }

            return transactions;
        }

        private decimal ConvertMilliUnitsToDecimal(int amount) => (decimal)amount / 1000;
    }
}
