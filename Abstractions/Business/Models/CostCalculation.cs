using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class CostCalculation
    {
        /// <summary>
        /// Total cost
        /// </summary>
        [JsonPropertyName("total")]
        public Money Total { get; set; }
        /// <summary>
        /// Shipping cost
        /// </summary>
        [JsonPropertyName("shipping")]
        public Money Shipping { get; set; }
        /// <summary>
        /// Value-added tax
        /// </summary>
        [JsonPropertyName("vat")]
        public Money Vat { get; set; }
    }

    /// <summary>
    /// Unified costing container
    /// </summary>
    /// <example>the result of a shopping cart costing</example>
    public class CostCalculationTotal : CostCalculation
    {
        /// <summary>
        /// Cost calculation identifier. For example, in case of HLF: pre-order number
        /// </summary>
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
        /// <summary>
        /// Date and time of the calculation, UTC
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("discountPercent")]
        public int DiscountPercent { get; set; }
        /// <summary>
        /// Сost breakdown. Summ of the items must be equal to <see cref="Total"/>
        /// </summary>
        /// <remarks>identifier is a product or a service that makes the expense</remarks>
        [JsonPropertyName("items")]
        public IEnumerable<(string identifier, CostCalculation cost)> Items { get; set; }
        /// <summary>
        /// Total taxes, discounts, charges and other explanations for price calculation
        /// </summary>
        [JsonPropertyName("details")]
        public IEnumerable<(string identifier, Money cost)> Details { get; set; }
        /// <summary>
        /// Additional parameters that take participation in calculation process
        /// </summary>
        /// <example>HLF: kiosk mode, month (in case of dual month period), consumption type...</example>
        [JsonPropertyName("additionalParams")]
        public Dictionary<string, string> AdditionalParams { get; set; } = new Dictionary<string, string>();
    }
}