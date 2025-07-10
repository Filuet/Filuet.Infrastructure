using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class CostCalculation
    {
        [JsonPropertyName("sku")]
        public string Sku { get; set; }
        [JsonPropertyName("qty")]
        public int Quantity { get; set; }
        /// <summary>
        /// Total cost
        /// </summary>
        [JsonPropertyName("total")]
        public Money Total { get; set; }
        /// <summary>
        /// Shipping cost
        /// </summary>
        [JsonPropertyName("shipping")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Money Shipping { get; set; }
        /// <summary>
        /// Value-added tax
        /// </summary>
        [JsonPropertyName("vat")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Money Vat { get; set; }

        [JsonPropertyName("additionalParams")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string> AdditionalParams { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// Unified costing container
    /// </summary>
    /// <example>the result of a shopping cart costing</example>
    public class CostCalculationTotal
    {
        /// <summary>
        /// Cost calculation identifier. For example, in case of HLF: pre-order number
        /// </summary>
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
        /// <summary>
        /// Customer identifier
        /// </summary>
        [JsonPropertyName("customer")]
        public string Customer { get; set; }
        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Date and time of the calculation, UTC
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Total cost
        /// </summary>
        [JsonPropertyName("total")]
        public Money Total { get; set; }
        /// <summary>
        /// Shipping cost
        /// </summary>
        [JsonPropertyName("shipping")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Money Shipping { get; set; }
        /// <summary>
        /// Value-added tax
        /// </summary>
        [JsonPropertyName("vat")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Money Vat { get; set; }
        [JsonPropertyName("discountPercent")]
        public int DiscountPercent { get; set; }
        /// <summary>
        /// Сost breakdown. Summ of the items must be equal to <see cref="Total"/>
        /// </summary>
        /// <remarks>identifier is a product or a service that makes the expense</remarks>
        [JsonPropertyName("items")]
        public IEnumerable<CostCalculation> Items { get; set; }
        /// <summary>
        /// Total taxes, discounts, charges and other explanations for price calculation
        /// </summary>
        [JsonPropertyName("details")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<PricingDetails> Details { get; set; }
        /// <summary>
        /// Additional parameters that take participation in calculation process
        /// </summary>
        /// <example>HLF: kiosk mode, month (in case of dual month period), consumption type...</example>
        [JsonPropertyName("additionalParams")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string> AdditionalParams { get; set; } = new Dictionary<string, string>();

        public Cart ToCart()
            => new Cart {
                Items = Items.Select(x => new CartItem { Sku = x.Sku, Quantity = x.Quantity }),
                AdditionalParams = AdditionalParams
            };
    }
}