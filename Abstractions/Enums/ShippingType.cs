using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum ShippingType : byte
    {
        [Code("store")]
        Store = 0x01,
        [Code("courier")]
        CourierDelivery = 0x02
    }
}
