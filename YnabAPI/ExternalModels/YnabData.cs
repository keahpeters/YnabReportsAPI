using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace YnabAPI.ExternalModels
{
    public class YnabData
    {
        [JsonPropertyName("transactions")]
        public IEnumerable<YnabTransaction> Transactions { get; set; } = default!;
    }
}
