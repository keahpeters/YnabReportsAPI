using System.Text.Json.Serialization;

namespace YnabAPI.ExternalModels
{
    public class YnabResponse
    {
        [JsonPropertyName("data")]
        public YnabData Data { get; set; } = default!;
    }
}
