using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace YnabAPI.ExternalModels
{
    public class YnabTransaction
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public int Amount { get; set; }

        public string Cleared { get; set; } = default!;

        public bool Approved { get; set; }

        [JsonPropertyName("account_name")]
        public string AccountName { get; set; } = default!;

        [JsonPropertyName("payee_name")]
        public string? PayeeName { get; set; }

        [JsonPropertyName("category_name")]
        public string CategoryName { get; set; } = default!;

        public IEnumerable<YnabTransaction> SubTransactions { get; set; } = default!;
    }
}
