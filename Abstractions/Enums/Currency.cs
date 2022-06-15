using Filuet.Infrastructure.Attributes;
using System.ComponentModel;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    /// <summary>
    /// ISO 4217
    /// </summary>
    public enum Currency : short
    {
        [Code("AMD")]
        [Description("֏")]
        ArmenianDram = 51,
        [Code("KHR")]
        [Description("៛")]
        CambodianRiel = 116,
        [Code("INR")]
        [Description("₹")]
        IndianRupee = 356,
        [Code("IDR")]
        [Description("Rp")]
        IndonesianRupiah = 360,
        [Code("NIS")]
        [Description("₪")]
        IsraeliNewShekel = 376,
        [Code("KZT")]
        [Description("₸")]
        KazakhstaniTenge = 398,
        [Code("RUB")]
        [Description("₽")]
        RussianRuble = 643,
        [Code("USD")]
        [Description("$")]
        UnitedStatesDollar = 840,
        [Code("UZS")]
        [Description("so'm")]
        UzbekistanSom = 860,
        [Code("AZN")]
        [Description("₼")]
        AzerbaijaniManat = 944,
        [Code("EUR")]
        [Description("€")]
        Euro = 978,
        [Code("GEL")]
        [Description("₾")]
        GeorgianLari = 981
    }
}