using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum PaymentMethod : short
    {
        [Code("cash")]
        Cash = 0x01,
        [Code("card")]
        Card,
        [Code("digital")] // other cashless payments
        Digital
    }
}