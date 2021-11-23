using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Business;
using Filuet.Infrastructure.Ordering.Converters;
using Filuet.Infrastructure.Ordering.Enums;

namespace Filuet.Infrastructure.Ordering.Models
{
    public class Order
    {
        [JsonIgnore]
        public Guid Id { get => _id; }

        public string Number { get; internal set; }

        public string Customer { get; internal set; }

        public string CustomerName { get; internal set; }

        public Country Location { get; internal set; }

        public Language Language { get; internal set; }

        public DateTime Timestamp { get => _timestamp; }

        public decimal Points { get; internal set; }

        /// <summary>
        /// Extra data such as a selected month, kiosk identifier e.t.c.
        /// </summary>
        public Dictionary<string, object> ExtraData { get; internal set; }

        [JsonConverter(typeof(GoodsObtainingMethodConverter))]
        public GoodsObtainingMethod Obtaining { get; internal set; } = GoodsObtainingMethod.Warehouse;

        internal Order()
        {
            _id = Guid.NewGuid();
            _timestamp = DateTime.Now;
        }

        /// <summary>
        /// Order total
        /// </summary>
        public Money Amount { get; internal set; }

        public IEnumerable<OrderLine> Items { get; internal set; }

        public override string ToString() => JsonSerializer.Serialize(this, typeof(object), JsonSerializationOptions.EventPrettyOptions);

        private readonly Guid _id;

        private readonly DateTime _timestamp;
    }
}
