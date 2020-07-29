using System.Text.Json.Serialization;

namespace YnabReportsAPI.YnabAPI.ExternalModels
{
    public class YnabSubTransaction
    {
        public string Id { get; set; } = default!;

        public int Amount { get; set; }

        [JsonPropertyName("payee_name")]
        public string? PayeeName { get; set; }

        [JsonPropertyName("category_name")]
        public string CategoryName { get; set; } = default!;

        public bool Deleted { get; set; }
    }
}
