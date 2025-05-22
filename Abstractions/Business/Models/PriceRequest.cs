using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class CartPriceRequest
    {
        /// <summary>
        /// Identifier of the price
        /// </summary>
        /// <example>HLF: pre-order number</example>
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        [JsonPropertyName("items")]
        public Dictionary<string, int> Items { get; set; }
        /// <summary>
        /// Date-time of the calculation
        /// </summary>
        [JsonPropertyName("date")]
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
        /// <summary>
        /// Additional parameters that take participation in calculation process
        /// </summary>
        /// <example>HLF: kiosk mode, month (in case of dual month period), consumption type...</example>
        [JsonPropertyName("additionalParams")]
        public Dictionary<string, string> AdditionalParams { get; set; } = new Dictionary<string, string>();
    }
}
