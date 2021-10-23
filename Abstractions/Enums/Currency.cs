using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    /// <summary>
    /// ISO 4217
    /// </summary>
    public enum Currency : ushort
    {
        [Code("INR")]
        IndianRupee = 356,
        [Code("RUB")]
        RussianRuble = 643,
        [Code("USD")]
        UnitedStatesDollar = 840,
        [Code("EUR")]
        Euro = 978,
        [Code("GEL")]
        GeorgianLari = 981
    }
}