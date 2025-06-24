using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class SkuCheckResult
    {
        [JsonPropertyName("sku")]
        public string Sku { get; set; }
        [JsonPropertyName("availableQty")]
        public int AvailableQty { get; set; }
        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
