namespace Filuet.Infrastructure.Ordering.Dto
{
    public class OrderLineDto
    {
        public string ProductUID { get; set; }
        public string Name { get; set; }

        public uint Quantity { get; set; }

        /// <summary>
        /// Unit cost
        /// </summary>
        public MoneyDto Amount { get; set; }

        public MoneyDto TotalAmount { get; set; }

        /// <summary>
        /// Loyalty program points
        /// </summary>
        public decimal Points { get; set; }
    }
}