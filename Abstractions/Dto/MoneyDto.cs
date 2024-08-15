using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Dto
{
    public class MoneyDto
    {
        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [JsonPropertyName("curr")]
        public string Currency { get; set; }
    }
}