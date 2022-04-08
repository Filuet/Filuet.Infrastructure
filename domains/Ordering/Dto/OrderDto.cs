using Filuet.Infrastructure.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Ordering.Dto
{
    public class OrderDto
    {
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
        public PaymentMethod? PaymentMethod { get; set; }

        [JsonPropertyName("total")]
        /// <summary>
        /// Order total
        /// </summary>
        public MoneyDto Amount { get; set; }

        [JsonPropertyName("paid")]
        /// <summary>
        /// Paid amount
        /// </summary>
        public MoneyDto Paid { get; set; }

        [JsonPropertyName("change")]
        /// <summary>
        /// Paid amount
        /// </summary>
        public MoneyDto Change { get; set; }

        [JsonPropertyName("changeGiven")]
        /// <summary>
        /// Change given amount
        /// </summary>
        public MoneyDto ChangeGiven { get; set; }


        [JsonPropertyName("items")]
        public IEnumerable<OrderLineDto> Items { get; set; }

        [JsonPropertyName("uncollected")]
        public IEnumerable<OrderItemDto> UncollectedItems { get; set; }
    }
}