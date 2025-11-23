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
        /// <summary>
        /// Weight in gramms
        /// </summary>
        [JsonPropertyName("weight")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull)]
        public int Weight { get; set; } = 0;
        [JsonPropertyName("loc")]
        public List<ProductLocalization> Localization { get; set; }
        [JsonPropertyName("price")]
        public Money Price { get; set; }
        /// <summary>
        /// Additional parameters that take participation in ordering process
        /// </summary>
        /// <example>HLF: vp, product type</example>
        [JsonPropertyName("additionalParams")]
        public Dictionary<string, string> AdditionalParams { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Display number
        /// </summary>
        [JsonPropertyName("idx")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull)]
        public int Index { get; set; }

        public ProductLocalization this[Language language]
            => Localization.FirstOrDefault(x => x.Language == language)
                ?? new ProductLocalization {
                    Name = Sku,
                    Description = string.Empty,
                    Language = Language.English
                };

        public override string ToString()
            => $"{Sku} {Price}";
    }
}
