using Filuet.Infrastructure.Abstractions.Converters;
using Filuet.Infrastructure.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    [JsonConverter(typeof(CountryJsonConverter))]
    public enum Country : short
    {
        [Code("AZ")]
        //[Description("AZE")]
        [Display(Name = "Azərbaycan Respublikası", Description = "AZE")]
        Azerbaijan = 31,
        [Code("AU")]
        //[Description("AUS")]
        [Display(Name = "Australia", Description = "AUS")]
        Australia = 36,
        [Code("AM")]
        //[Description("ARM")]
        [Display(Name = "Հայաստանի Հանրապետություն", Description = "ARM")]
        Armenia = 51,
        [Code("KH")]
        [Description("KHM")]
        [Display(Name = "កម្ពុជា", Description = "KHM")]
        Cambodia = 116,
        [Code("TW")]
        //[Description("TWN")]
        [Display(Name = "Tâi-uân", Description = "TWN")]
        Taiwan = 158,
        [Code("CY")]
        //[Description("CYP")]
        [Display(Name = "Κύπρος", Description = "CYP")]
        Cyprus = 196,
        [Code("EE")]
        //[Description("EST")]
        [Display(Name = "Eesti", Description = "EST")]
        Estonia = 233,
        [Code("FR")]
        //[Description("FRA")]
        [Display(Name = "la République Française", Description = "FRA")]
        France = 250,   
        [Code("GE")]
        //[Description("GEO")]
        [Display(Name = "საქართველო", Description = "GEO")]
        Georgia = 268,
        [Code("IN")]
        //[Description("IND")]
        [Display(Name = "भारत", Description = "IND")]
        India = 356,
        [Code("ID")]
        //[Description("IDN")]
        [Display(Name = "Republik Indonesia", Description = "IDN")]
        Indonesia = 360,
        [Code("IL")]
        //[Description("ISR")]
        [Display(Name = "ישראל", Description = "ISR")]
        Israel = 376,
        [Code("IT")]
        //[Description("ITA")]
        [Display(Name = "Repubblica Italiana", Description = "ITA")]
        Italy = 380,
        [Code("JP")]
        //[Description("JPN")]
        [Display(Name = "にほん", Description = "JPN")]
        Japan = 392,
        [Code("KZ")]
        //[Description("KAZ")]
        [Display(Name = "Қазақстан", Description = "KAZ")]
        Kazakhstan = 398,
        [Code("KR")]
        [Description("KOR")]
        [Display(Name = "한국", Description = "KOR")]
        Korea = 410,
        [Code("LV")]
        //[Description("LVA")]
        [Display(Name = "Latvija", Description = "LVA")]
        Latvia = 428,
        [Code("LT")]
        //[Description("LTU")]
        [Display(Name = "Lietuva", Description = "LTU")]
        Lithuania = 440,
        [Code("MY")]
        //[Description("MYS")]
        [Display(Name = "Malaysia", Description = "MYS")]
        Malaysia = 458,
        [Code("MQ")]
        //[Description("MTQ")]
        [Display(Name = "Martinique", Description = "MTQ")]
        Martinique = 474,
        [Code("RU")]
        //[Description("RUS")]
        [Display(Name = "Российская Федерация", Description = "RUS")]
        Russia = 643,
        [Code("VN")]
        //[Description("VNM")]
        [Display(Name = "Việt Nam", Description = "VNM")]
        Vietnam = 704,        
        [Code("ES")]
        //[Description("ESP")]
        [Display(Name = "España", Description = "ESP")]
        Spain = 724,
        [Code("US")]
        //[Description("USA")]
        [Display(Name = "USA", Description = "USA")]
        USA = 840,
        [Code("UZ")]
        //[Description("UZB")]
        [Display(Name = "Oʻzbekiston", Description = "UZB")]
        Uzbekistan = 860
    }
}