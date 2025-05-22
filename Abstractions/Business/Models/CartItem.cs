using System;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class CartItem
    {
        [JsonPropertyName("sku")]
        public string Sku { get; private set; }
        [JsonPropertyName("qty")]
        public int Quantity { get; private set; }

        public static CartItem Create(string sku, int quantity) {
            if (string.IsNullOrWhiteSpace(sku) || sku.Trim().Length < 3)
                throw new ArgumentException("Invalid sku");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");

            return new CartItem { Sku = sku.Trim(), Quantity = quantity };
        }

        public override string ToString()
            => $"{Sku} x{Quantity}";
    }
}