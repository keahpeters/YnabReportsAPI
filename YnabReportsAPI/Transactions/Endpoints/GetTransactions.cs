using System.Collections.Generic;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using YnabReportsAPI.Transactions.Models;
using YnabReportsAPI.Transactions.Services;
using YnabReportsAPI.Transactions.ViewModels;

namespace YnabReportsAPI.Transactions.Endpoints
{
    public class GetTransactions : BaseAsyncEndpoint
    {
        private readonly ITransactionService transactionService;
        private readonly IMapper mapper;

        public GetTransactions(ITransactionService transactionService, IMapper mapper)
        {
            this.transactionService = transactionService;
            this.mapper = mapper;
        }

        [HttpGet("/v1/budgets/{budgetId}/transactions")]
        [SwaggerOperation(
            Summary = "Get a list of transactions",
            Description = "Get a list of transactions",
            OperationId = "Transactions.Get",
            Tags = new[] { "TransactionEndpoint" })
        ]
        public async Task<ActionResult<IEnumerable<TransactionViewModel>>> HandleAsync(string budgetId, [FromQuery] GetTransactionsViewModel queryStringParameters)
        {
            IEnumerable<Transaction> transactions = await this.transactionService.GetTransactions(budgetId, queryStringParameters?.StartDate);
            return this.Ok(mapper.Map<TransactionViewModel[]>(transactions));
        }
    }
}
