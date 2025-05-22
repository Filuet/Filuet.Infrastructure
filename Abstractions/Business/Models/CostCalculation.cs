using System;
using System.Collections.Generic;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    /// <summary>
    /// Unified costing container
    /// </summary>
    /// <example>the result of a shopping cart costing</example>
    public class CostCalculation
    {
        /// <summary>
        /// Date and time of the calculation, UTC
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Total cost
        /// </summary>
        public Money Total { get; set; }
        /// <summary>
        /// Shipping cost
        /// </summary>
        public Money Shipping { get; set; }
        /// <summary>
        /// Value-added tax
        /// </summary>
        public Money Vat { get; set; }
        /// <summary>
        /// Total discount
        /// </summary>
        public Money Discount { get; set; }
        /// <summary>
        /// Сost breakdown. Summ of the items must be equal to <see cref="Total"/>
        /// </summary>
        /// <remarks>identifier is a product or a service that makes the expense</remarks>
        public IEnumerable<(string identifier, CostCalculation cost)> Items { get; set; }
        /// <summary>
        /// Total taxes, discounts, charges and other explanations for price calculation
        /// </summary>
        public IEnumerable<(string identifier, Money cost)> Details { get; set; }
    }
}