using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum ShippingType : short
    {
        [Code("store")]
        Store = 0x01,
        [Code("courier")]
        CourierDelivery = 0x02,
        [Code("locker")]
        Locker = 0x03
    }
}