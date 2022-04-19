using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Ordering.Dto
{
    public class OrderLineDto
    {
        [JsonPropertyName("uid")]
        public string ProductUID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("qty")]
        public uint Quantity { get; set; }

        [JsonPropertyName("dueAmount")]
        /// <summary>
        /// Unit cost
        /// </summary>
        public MoneyDto DueAmount { get; set; }

        [JsonPropertyName("totalAmount")]
        public MoneyDto TotalAmount { get; set; }

        [JsonPropertyName("points")]
        /// <summary>
        /// Loyalty program points
        /// </summary>
        public decimal Points { get; set; }
    }
}