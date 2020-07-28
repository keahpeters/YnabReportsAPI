using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace YnabReportsAPI.YnabAPI.ExternalModels
{
    public class YnabData
    {
        [JsonPropertyName("transactions")]
        public IEnumerable<YnabTransaction>? Transactions { get; set; }
    }
}
