using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Ordering.Enums
{
    public enum GoodsObtainingMethod
    {
        [Code("WH")]
        /// <summary>
        /// Fetch products from warehouse
        /// </summary>
        Warehouse = 0x01,
        [Code("AS")]
        /// <summary>
        /// Fetch products from dispensing machine
        /// </summary>
        Dispensing
    }
}
