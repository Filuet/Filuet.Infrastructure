using System;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class CartItem
    {
        [JsonPropertyName("sku")]
        public string Sku { get; set; }
        [JsonPropertyName("qty")]
        public int Quantity { get; set; }
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        public static CartItem Create(string sku, int quantity = 1) {
            if (string.IsNullOrWhiteSpace(sku) || (sku.Trim().Length < 3 && sku.Trim().ToLower() != "it")) // users may use a pronounce to mention the product
                throw new ArgumentException("Invalid sku");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");

            return new CartItem { Sku = sku.Trim(), Quantity = quantity, Timestamp = DateTime.UtcNow };
        }

        public static CartItem Load(string sku, int quantity, DateTime timestamp) {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");

            return new CartItem { Sku = sku.Trim(), Quantity = quantity, Timestamp = timestamp };
        }

        public override string ToString()
            => $"{Sku ?? "<null>"} x{Quantity}";
    }
}