using System;
using System.Collections.Generic;

namespace Filuet.Infrastructure.Ordering.Dto
{
    public class OrderDto
    {
        public string Number { get; internal set; }

        public string Customer { get; internal set; }

        public string CustomerName { get; internal set; }

        public string CountryCode { get; internal set; }

        public string LanguageCode { get; internal set; }

        public DateTime Date { get; internal set; }

        public decimal Points { get; internal set; }

        /// <summary>
        /// Extra data such as a selected month, kiosk identifier e.t.c.
        /// </summary>
        public Dictionary<string, object> ExtraData { get; internal set; }

        public string Obtaining { get; set; }

        /// <summary>
        /// Order total
        /// </summary>
        public MoneyDto Amount { get; internal set; }

        /// <summary>
        /// Paid amount
        /// </summary>
        public MoneyDto Paid { get; internal set; }

        public IEnumerable<OrderLineDto> Items { get; internal set; }

        public IEnumerable<OrderItemDto> UncollectedItems { get; internal set; }
    }
}