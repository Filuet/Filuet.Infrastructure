using Filuet.Infrastructure.Abstractions.Business;
using System;

namespace Filuet.Infrastructure.Ordering.Models
{
    public class OrderLine : OrderItem
    {
        /// <summary>
        /// Unit cost
        /// </summary>
        public Money Amount { get; protected set; }

        public Money TotalAmount { get; protected set; }

        private OrderLine() : base() { }

        /// <summary>
        /// Create order line
        /// </summary>
        /// <param name="productUid">Unique identifier of product. E.g. SKU</param>
        /// <param name="quantity">quantity of order line</param>
        /// <param name="amount">Unit cost</param>
        /// <param name="totalAmount">Total cost of line</param>
        /// <returns></returns>
        public static OrderLine Create(string productUid, uint quantity, Money amount, Money totalAmount)
        {
            OrderItem item = OrderItem.Create(productUid, quantity);

            if (amount == null || amount.Value < 0m)
                throw new ArgumentException("Amount is mandatory");

            if (totalAmount == null || totalAmount.Value < 0m)
                throw new ArgumentException("Total amount is mandatory");

            return new OrderLine { ProductUID = item.ProductUID, Quantity = item.Quantity, Amount = amount, TotalAmount = totalAmount };
        }

        /// <summary>
        /// Create order line with quantity equals to 1
        /// </summary>
        /// <param name="productUid">Unique identifier of product. E.g. SKU</param>
        /// <param name="amount">Unit cost</param>
        /// <returns></returns>
        new public static OrderLine Create(string productUid, Money amount)
        {
            OrderItem item = OrderItem.Create(productUid, 1);

            return new OrderLine { ProductUID = item.ProductUID, Quantity = item.Quantity, Amount = amount, TotalAmount = amount };
        }

        public override string ToString() => $"{base.ToString()} x{Quantity}";
    }
}
