using Filuet.Infrastructure.Abstractions.Converters;
using Filuet.Infrastructure.Attributes;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    [JsonConverter(typeof(CountryJsonConverter))]
    public enum Country : short
    {
        [Code("AZ")]
        [Description("AZE")]
        Azerbaijan = 31,
        [Code("AM")]
        [Description("ARM")]
        Armenia = 51,
        [Code("KH")]
        [Description("KHM")]
        Cambodia = 116,
        [Code("TW")]
        [Description("TWN")]
        Taiwan = 158,
        [Code("EE")]
        [Description("EST")]
        Estonia = 233,        
        [Code("FR")]
        [Description("FRA")]
        France = 250,   
        [Code("GE")]
        [Description("GEO")]
        Georgia = 268,
        [Code("IN")]
        [Description("IND")]
        India = 356,
        [Code("ID")]
        [Description("IDN")]
        Indonesia = 360,
        [Code("IL")]
        [Description("ISR")]
        Israel = 376,         
        [Code("KZ")]
        [Description("KAZ")]
        Kazakhstan = 398,
        [Code("KR")]
        [Description("KOR")]
        Korea = 410,
        [Code("LV")]
        [Description("LVA")]
        Latvia = 428,
        [Code("LT")]
        [Description("LTU")]
        Lithuania = 440,
        [Code("MY")]
        [Description("MYS")]
        Malaysia = 458,
        [Code("MQ")]
        [Description("MTQ")]
        Martinique = 474,
        [Code("RU")]
        [Description("RUS")]
        Russia = 643,
        [Code("VN")]
        [Description("VNM")]
        Vietnam = 704,
        [Code("US")]
        [Description("USA")]
        USA = 840,
        [Code("UZ")]
        [Description("UZB")]
        Uzbekistan = 860
    }
}