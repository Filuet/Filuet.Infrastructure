using Filuet.Infrastructure.Abstractions.Enums;
using System;

namespace Filuet.Infrastructure.Abstractions.Helpers
{
    public static class CountryHelpers
    {
        public static Currency GetCurrency(this Country country) {
            switch (country) {
                case Country.Armenia:
                    return Currency.ArmenianDram;
                case Country.Australia:
                    return Currency.AustralianDollar;
                case Country.Azerbaijan:
                    return Currency.AzerbaijaniManat;
                case Country.Cambodia:
                case Country.USA:
                    return Currency.UnitedStatesDollar;
                case Country.Cyprus:
                case Country.Latvia:
                case Country.Estonia:
                case Country.Lithuania:
                case Country.France:
                case Country.Martinique:
                case Country.Spain:
                case Country.Italy:
                    return Currency.Euro;
                case Country.Georgia:
                    return Currency.GeorgianLari;
                case Country.India:
                    return Currency.IndianRupee;
                case Country.Indonesia:
                    return Currency.IndonesianRupiah;
                case Country.Israel:
                    return Currency.IsraeliNewShekel;
                case Country.Japan:
                    return Currency.JapaneseYen;
                case Country.Kazakhstan:
                    return Currency.KazakhstaniTenge;
                case Country.Korea:
                    return Currency.SouthKoreanWon;
                case Country.Malaysia:
                    return Currency.MalaysianRinggit;
                case Country.Russia:
                    return Currency.RussianRuble;
                case Country.Taiwan:
                    return Currency.NewTaiwanDollar;
                case Country.Uzbekistan:
                    return Currency.UzbekistanSom;
                case Country.Vietnam:
                    return Currency.VietnameseDong;
                default:
                    throw new ArgumentException("No currency allocated for this country yet");
            }
        }
    }
}
