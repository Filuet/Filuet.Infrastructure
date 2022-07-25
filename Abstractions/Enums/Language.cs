using Filuet.Infrastructure.Abstractions.Converters;
using Filuet.Infrastructure.Attributes;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    [JsonConverter(typeof(LanguageJsonConverter))]
    public enum Language
    {
        [Code("en")] // ISO lang name: two letter
        [Description("enGB")] // IETF tag
        English,
        [Code("ru")]
        [Description("ruRu")]
        Russian,
        [Code("he")]
        [Description("he")]
        Hebrew,
        [Code("lv")]
        [Description("lvLV")]
        Latvian,
        [Code("et")]
        [Description("etEE")]
        Estonian,
        [Code("lt")]
        [Description("ltLT")]
        Lithuanian,
        [Code("hi")]
        [Description("hiIN")]
        Hindi,
        [Code("vi")]
        [Description("viVN")]
        Vietnamese,
        [Code("ar")]
        [Description("ar")]
        Arabic,
        [Code("es")]
        [Description("esES")]
        Spanish,
        [Code("it")]
        [Description("itIT")]
        Italian,
        [Code("zh")]
        [Description("zhCN")]
        Chinese,
        [Code("ko")]
        [Description("koKR")]
        Korean,
        [Code("fr")]
        [Description("frFR")]
        French,
        [Code("ms")]
        [Description("msMY")]
        Malay,
        [Code("id")]
        [Description("idID")]
        Indonesian,
        [Code("ge")]
        [Description("kaGE")]
        Georgian,
        [Code("km")]
        [Description("kmKH")]
        Khmer,
        [Code("kk")]
        [Description("kkKZ")]
        Kazakh,
        [Code("el")]
        [Description("elGR")]
        Greek,
        [Code("tr")]
        [Description("trTR")]
        Turkish
    }
}