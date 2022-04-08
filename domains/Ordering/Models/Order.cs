using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Business;
using Filuet.Infrastructure.Ordering.Converters;
using Filuet.Infrastructure.Ordering.Enums;
using Filuet.Infrastructure.Abstractions.Converters;

namespace Filuet.Infrastructure.Ordering.Models
{
    public class Order
    {
        [JsonIgnore]
        public Guid Id { get => _id; }

        [JsonPropertyName("isCrash")]
        public bool IsCrash { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("customer")]
        public string Customer { get; set; }

        [JsonPropertyName("name")]
        public string CustomerName { get; set; }

        [JsonPropertyName("country")]
        [JsonConverter(typeof(CountryJsonConverter))]
        public Country Location { get; set; }

        [JsonPropertyName("language")]
        [JsonConverter(typeof(LanguageJsonConverter))]
        public Language Language { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonIgnore]
        public DateTime Timestamp { get => _timestamp; }

        [JsonPropertyName("points")]
        public decimal Points { get; set; }

        [JsonPropertyName("extra")]
        /// <summary>
        /// Extra data such as a selected month, kiosk identifier e.t.c.
        /// </summary>
        public Dictionary<string, object> ExtraData { get; set; }

        [JsonPropertyName("obtainMethod")]
        [JsonConverter(typeof(GoodsObtainingMethodConverter))]
        public GoodsObtainingMethod Obtaining { get; set; } = GoodsObtainingMethod.Warehouse;

        [JsonPropertyName("paymentMethod")]
        public PaymentMethod? PaymentMethod { get; set; }

        public Order()
        {
            _id = Guid.NewGuid();
            _timestamp = DateTime.Now;
        }

        [JsonPropertyName("total")]
        /// <summary>
        /// Order total
        /// </summary>
        public Money Total { get; set; }

        [JsonPropertyName("paid")]
        /// <summary>
        /// Paid amount
        /// </summary>
        public Money Paid { get; set; }

        [JsonPropertyName("change")]
        /// <summary>
        /// Change amount
        /// </summary>
        public Money Change { get; set; }

        [JsonPropertyName("changeGiven")]
        /// <summary>
        /// Change given amount
        /// </summary>
        public Money ChangeGiven { get; set; }

        [JsonPropertyName("items")]
        public IEnumerable<OrderLine> Items { get; set; }

        [JsonPropertyName("uncollected")]
        public IEnumerable<OrderItem> UncollectedItems { get; set; }

        public static Order Deserialize(string serialized)
            => JsonSerializer.Deserialize<Order>(serialized);

        public override string ToString() => JsonSerializer.Serialize(this, typeof(object), JsonSerializationOptions.EventPrettyOptions);

        private readonly Guid _id;

        private readonly DateTime _timestamp;
    }
}