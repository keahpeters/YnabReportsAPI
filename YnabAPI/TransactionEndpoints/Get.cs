using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

using Ardalis.ApiEndpoints;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using YnabAPI.Services;
using YnabAPI.TransactionEndpoints.Models;

namespace YnabAPI.TransactionEndpoints
{
    public class Get : BaseAsyncEndpoint
    {
        private readonly ITransactionService transactionService;

        public Get(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet("/budgets/{budgetId}/transactions")]
        [SwaggerOperation(
            Summary = "Get a list of transactions",
            Description = "Get a list of transactions",
            OperationId = "Transactions.Get",
            Tags = new[] { "TransactionEndpoint" })
        ]
        public async Task<ActionResult<IEnumerable<Transaction>>> HandleAsync(string budgetId, [FromQuery] GetTransactionsQueryString queryStringParameters)
        {
            try
            {
                var transactions = await this.transactionService.GetTransactions(budgetId, queryStringParameters?.StartDate);
                return this.Ok(transactions);
            }
            catch
            {
                // todo: improve error handling
                return this.StatusCode(500);
            }
        }
    }
}
