using Filuet.Infrastructure.Attributes;
using System.ComponentModel;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    /// <summary>
    /// ISO 4217
    /// </summary>
    public enum Currency : short
    {
        [Code("AUD")]
        [Description("$")]
        AustralianDollar = 36,
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
        [Code("ILS")]
        [Description("₪")]
        IsraeliNewShekel = 376,
        [Code("JPY")]
        [Description("¥")]
        JapaneseYen = 392,
        [Code("KZT")]
        [Description("₸")]
        KazakhstaniTenge = 398,        
        [Code("KRW")]
        [Description("₩")]
        SouthKoreanWon = 410,
        [Code("RUB")]
        [Description("₽")]
        RussianRuble = 643,
        [Code("VND")]
        [Description("₫")]
        VietnameseDong = 704,
        [Code("USD")]
        [Description("$")]
        UnitedStatesDollar = 840,
        [Code("UZS")]
        [Description("so'm")]
        UzbekistanSom = 860,
        [Code("TWD")]
        [Description("NT$")]
        NewTaiwanDollar = 901,
        [Code("AZN")]
        [Description("₼")]
        AzerbaijaniManat = 944,
        [Code("EUR")]
        [Description("€")]
        Euro = 978,
        [Code("GEL")]
        [Description("₾")]
        GeorgianLari = 981,
        [Code("MYR")]
        [Description("RM")]
        MalaysianRinggit = 458

    }
}