using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

using Ardalis.ApiEndpoints;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using YnabAPI.Services;

namespace YnabAPI.Endpoints
{
    public class GetTransactionsEndpoint : BaseAsyncEndpoint
    {
        private readonly ITransactionService transactionService;

        public GetTransactionsEndpoint(ITransactionService transactionService)
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
        public async Task<ActionResult<IEnumerable<Transaction>>> HandleAsync(string budgetId, string? startDate = default)
        {
            try
            {
                var transactions = await this.transactionService.GetTransactions(budgetId, startDate);
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
