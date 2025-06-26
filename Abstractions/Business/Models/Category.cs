using Filuet.Infrastructure.Abstractions.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class Category
    {
        [JsonPropertyName("loc")]
        public IEnumerable<CategoryLocalization> Localization { get; set; }
        [JsonPropertyName("products")]
        public IEnumerable<Product> Products { get; set; }

        public string this[Language language]
          => Localization.FirstOrDefault(x => x.Language == language)?.Name ?? Localization.First()?.Name;
    }
}
