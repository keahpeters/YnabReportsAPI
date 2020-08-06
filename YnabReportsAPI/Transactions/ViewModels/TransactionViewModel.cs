using System;
using System.Text.Json.Serialization;

using YnabReportsAPI.JsonConverters;

namespace YnabReportsAPI.Transactions.ViewModels
{
    public class TransactionViewModel
    {
        [JsonConverter(typeof(DateFormatJsonConverter))]
        public DateTime TransactionDate { get; set; }

        public decimal Amount { get; set; }

        public string? Category { get; set; }
    }
}
