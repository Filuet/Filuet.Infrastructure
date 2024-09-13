using Filuet.Infrastructure.Abstractions.Converters;
using Filuet.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    [JsonConverter(typeof(LanguageJsonConverter))]
    public enum Language : short
    {
        [Code("en")] // ISO lang name: two letter
        [Display(Name = "English", Description = "enGB")]
        English = 0x01,
        [Code("ru")]
        [Display(Name = "Русский", Description = "ruRU")]
        Russian,
        [Code("he")]
        [Display(Name = "עברית", Description = "heIL")]
        Hebrew,
        [Code("lv")]
        [Display(Name = "Latviešu", Description = "lvLV")]
        Latvian,
        [Code("et")]
        [Display(Name = "Eesti", Description = "etEE")]
        Estonian,
        [Code("lt")]
        [Display(Name = "Lietuvių", Description = "ltLT")]
        Lithuanian,
        [Code("hi")]
        [Display(Name = "हिन्दी", Description = "hiIN")]
        Hindi,
        [Code("vi")]
        [Display(Name = "Tiếng Việt", Description = "viVN")]
        Vietnamese,
        [Code("ar")]
        [Display(Name = "اللغة العربية", Description = "arAR")]
        Arabic,
        [Code("es")]
        [Display(Name = "Español", Description = "esES")]
        Spanish,
        [Code("it")]
        [Display(Name = "Italiano", Description = "itIT")]
        Italian,
        [Code("zh")]
        [Display(Name = "华语", Description = "zhCN")]
        Chinese,
        [Code("ko")]
        [Display(Name = "한국어", Description = "koKR")]
        Korean,
        [Code("fr")]
        [Display(Name = "Français", Description = "frFR")]
        French,
        [Code("ms")]
        [Display(Name = "Bahasa Malaysia", Description = "msMY")]
        Malay,
        [Code("id")]
        [Display(Name = "Bahasa Indonesia", Description = "idID")]
        Indonesian,
        [Code("ka")]
        [Display(Name = "ქართული", Description = "kaGE")]
        Georgian,
        [Code("km")]
        [Display(Name = "ភាសាខ្មែរ", Description = "kmKH")]
        Khmer,
        [Code("kk")]
        [Display(Name = "Қазақ", Description = "kkKZ")]
        Kazakh,
        [Code("el")]
        [Display(Name = "Ελληνικά", Description = "elGR")]
        Greek,
        [Code("tr")]
        [Display(Name = "Türkçe", Description = "trTR")]
        Turkish,
        [Code("ja")]
        [Display(Name = "日本語", Description = "jaJP")]
        Japanese,
        [Code("hy")]
        [Display(Name = "Հայերեն", Description = "hyAM")]
        Armenian
    }
}