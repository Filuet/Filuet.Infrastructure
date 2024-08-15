using Filuet.Infrastructure.Abstractions.Business;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Ordering.Dto
{
    public class OrderDto
    {
        [JsonPropertyName("isCrash")]
        public bool IsCrash { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("customer")]
        public string Customer { get; set; }

        [JsonPropertyName("name")]
        public string CustomerName { get; set; }

        [JsonPropertyName("country")]
        public string CountryCode { get; set; }

        [JsonPropertyName("language")]
        public string LanguageCode { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("points")]
        public decimal Points { get; set; }

        [JsonPropertyName("extra")]
        /// <summary>
        /// Extra data such as a selected month, kiosk identifier e.t.c.
        /// </summary>
        public Dictionary<string, object> ExtraData { get; set; }

        [JsonPropertyName("obtainMethod")]
        public string Obtaining { get; set; }

        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("total")]
        /// <summary>
        /// Order total
        /// </summary>
        public Money Amount { get; set; }

        [JsonPropertyName("paid")]
        /// <summary>
        /// Paid amount
        /// </summary>
        public Money Paid { get; set; }

        [JsonPropertyName("change")]
        /// <summary>
        /// Change to be given
        /// </summary>
        public Money Change { get; set; }

        [JsonPropertyName("changeGiven")]
        /// <summary>
        /// Change given amount
        /// </summary>
        public Money ChangeGiven { get; set; }

        [JsonPropertyName("items")]
        public IEnumerable<OrderLineDto> Items { get; set; }

        [JsonPropertyName("uncollected")]
        public IEnumerable<OrderItemDto> UncollectedItems { get; set; }

        [JsonPropertyName("installments")]
        public uint? Installments { get; set; }
    }
}