using System;
using System.Collections.Generic;

namespace Filuet.Infrastructure.Ordering.Dto
{
    public class OrderDto
    {
        public string Number { get; set; }

        public string Customer { get; set; }

        public string CustomerName { get; set; }

        public string CountryCode { get; set; }

        public string LanguageCode { get; set; }

        public DateTime Date { get; set; }

        public decimal Points { get; set; }

        /// <summary>
        /// Extra data such as a selected month, kiosk identifier e.t.c.
        /// </summary>
        public Dictionary<string, object> ExtraData { get; set; }

        public string Obtaining { get; set; }

        /// <summary>
        /// Order total
        /// </summary>
        public MoneyDto Amount { get; set; }

        /// <summary>
        /// Paid amount
        /// </summary>
        public MoneyDto Paid { get; set; }

        public IEnumerable<OrderLineDto> Items { get; set; }

        public IEnumerable<OrderItemDto> UncollectedItems { get; set; }
    }
}