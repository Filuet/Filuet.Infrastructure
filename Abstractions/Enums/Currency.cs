using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    /// <summary>
    /// ISO 4217
    /// </summary>
    public enum Currency : ushort
    {
        [Code("AMD")]
        ArmenianDram = 51,
        [Code("INR")]
        IndianRupee = 356,
        [Code("RUB")]
        RussianRuble = 643,
        [Code("USD")]
        UnitedStatesDollar = 840,
        [Code("UZS")]
        UzbekistanSom = 860,
        [Code("AZN")]
        AzerbaijaniManat = 944,
        [Code("EUR")]
        Euro = 978,
        [Code("GEL")]
        GeorgianLari = 981
    }
}