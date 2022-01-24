using System;

namespace Filuet.Infrastructure.Ordering.Models
{
    public class OrderItem
    {
        /// <summary>
        /// Unique identifier of product. E.g. SKU
        /// </summary>
        public string ProductUID { get; set; }

        public uint Quantity { get; set; }

        public OrderItem() { }

        /// <summary>
        /// Create single order line
        /// </summary>
        /// <param name="productUid">Unique identifier of product. E.g. SKU</param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static OrderItem Create(string productUid, uint quantity)
        {
            if (string.IsNullOrWhiteSpace(productUid))
                throw new ArgumentException("Product must be specified");

            if (quantity == 0)
                throw new ArgumentException("Quantity must be positive");

            return new OrderItem { ProductUID = productUid, Quantity = quantity };
        }

        public override string ToString() => $"{ProductUID} [{Quantity}]";
    }
}
