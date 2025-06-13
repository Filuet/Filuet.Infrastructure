using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class PricingDetails
    {
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
        [JsonPropertyName("cost")]
        public Money Cost { get; set; }
    }
}
