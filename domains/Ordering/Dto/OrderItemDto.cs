using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Ordering.Dto
{
    public class OrderItemDto
    {
        [JsonPropertyName("uid")]
        public string ProductUID { get; set; }

        [JsonPropertyName("qty")]
        public uint Quantity { get; set; }
    }
}