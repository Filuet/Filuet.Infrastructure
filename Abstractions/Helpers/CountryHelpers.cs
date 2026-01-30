using Filuet.Infrastructure.Abstractions.Enums;
using System;
using System.Linq;
using System.Linq.Expressions;

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

        public static Language[] GetLanguages(this Country country) {
            Language[] result = null;

            switch (country) {
                case Country.Armenia:
                    result = [Language.Armenian];
                    break;
                case Country.Latvia:
                case Country.Estonia:
                case Country.Lithuania:
                    result = [Language.Latvian, Language.Estonian, Language.Lithuanian];
                    break;
                case Country.France:
                case Country.Martinique:
                    result = [Language.French];
                    break;
                case Country.Georgia:
                    result = [Language.Georgian];
                    break;
                case Country.India:
                    result = [Language.Hindi, Language.Kannada];
                    break;
                case Country.Israel:
                    result = [Language.Hebrew, Language.Arabic, Language.Russian];
                    break;
                case Country.Japan:
                    result = [Language.Japanese];
                    break;
                case Country.Kazakhstan:
                    result = [Language.Kazakh, Language.Russian];
                    break;
                case Country.Korea:
                    result = [Language.Korean];
                    break;
                case Country.Malaysia:
                    result = [Language.Malay];
                    break;
                case Country.Russia:
                    result = [Language.Russian];
                    break;
                case Country.Taiwan:
                    result = [Language.Chinese];
                    break;
                case Country.Uzbekistan:
                    result = [Language.Uzbek, Language.Russian];
                    break;
                case Country.Vietnam:
                    result = [Language.Vietnamese];
                    break;
                default:
                    break;
            }

            return result.Concat([Language.English]).ToArray(); // English is possible by default
        }
    }
}
