using Filuet.Infrastructure.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class OrderImage
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        [JsonPropertyName("customer")]
        public string Customer { get; set; }
        [JsonPropertyName("total")]
        public Money Total { get; set; }
        [JsonPropertyName("country")]
        public Country Country { get; set; }
        [JsonPropertyName("txId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string PaymentTxId { get; set; }
        [JsonPropertyName("items")]
        public IEnumerable<CartItem> Items { get; set; }
        [JsonPropertyName("additionalParams")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string> AdditionalParams { get; set; } = new Dictionary<string, string>();
    }
}
