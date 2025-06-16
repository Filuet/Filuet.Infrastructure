using Filuet.Infrastructure.Abstractions.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class Product
    {
        [JsonPropertyName("sku")]
        public string Sku { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// Weight in gramms
        /// </summary>
        [JsonPropertyName("weight")]
        public int Weight { get; set; } = 0;
        [JsonPropertyName("desc")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("loc")]
        public List<ProductLocalization> Localization { get; set; }
        [JsonPropertyName("price")]
        public Money Price { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }
        /// <summary>
        /// Additional parameters that take participation in ordering process
        /// </summary>
        /// <example>HLF: vp, product type</example>
        [JsonPropertyName("additionalParams")]
        public Dictionary<string, object> AdditionalParams { get; set; } = new Dictionary<string, object>();

        public ProductLocalization this[Language language]
            => Localization.FirstOrDefault(x => x.Language == language)
                ?? new ProductLocalization { Name = Name, Description = Description, Language = Language.English };
    }
}
