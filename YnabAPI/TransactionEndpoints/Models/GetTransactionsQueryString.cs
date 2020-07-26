using System.ComponentModel.DataAnnotations;

namespace YnabAPI.TransactionEndpoints.Models
{
    public class GetTransactionsQueryString
    {
        public string? StartDate { get; set; }
    }
}
