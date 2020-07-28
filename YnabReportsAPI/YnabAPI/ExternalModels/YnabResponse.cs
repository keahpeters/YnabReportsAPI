using System.Text.Json.Serialization;

namespace YnabReportsAPI.YnabAPI.ExternalModels
{
    public class YnabResponse
    {
        [JsonPropertyName("data")]
        public YnabData? Data { get; set; }
    }
}
