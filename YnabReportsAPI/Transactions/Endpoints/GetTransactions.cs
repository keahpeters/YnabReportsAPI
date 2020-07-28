using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

using Ardalis.ApiEndpoints;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using YnabReportsAPI.Transactions.Services;
using YnabReportsAPI.Transactions.ViewModels;

namespace YnabReportsAPI.Transactions.Endpoints
{
    public class GetTransactions : BaseAsyncEndpoint
    {
        private readonly ITransactionService transactionService;

        public GetTransactions(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet("/v1/budgets/{budgetId}/transactions")]
        [SwaggerOperation(
            Summary = "Get a list of transactions",
            Description = "Get a list of transactions",
            OperationId = "Transactions.Get",
            Tags = new[] { "TransactionEndpoint" })
        ]
        public async Task<ActionResult<IEnumerable<Transaction>>> HandleAsync(string budgetId, [FromQuery] GetTransactionsViewModel queryStringParameters)
        {
            var transactions = await this.transactionService.GetTransactions(budgetId, queryStringParameters?.StartDate);
            return this.Ok(transactions);
        }
    }
}
