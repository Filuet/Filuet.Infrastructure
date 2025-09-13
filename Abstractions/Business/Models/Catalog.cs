using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class Catalog
    {
        [JsonPropertyName("categories")]
        public IEnumerable<Category> Categories { get; set; }

        [JsonIgnore]
        public Product this[string sku]
            => Categories.SelectMany(x => x.Products).Distinct().FirstOrDefault(x => string.Equals(x.Sku, sku.Trim(), System.StringComparison.InvariantCultureIgnoreCase));

        [JsonIgnore]
        public IEnumerable<Product> this[IEnumerable<string> skus]
            => Categories.SelectMany(x => x.Products).Distinct().Where(x => skus.Any(y => string.Equals(x.Sku, y.Trim(), System.StringComparison.InvariantCultureIgnoreCase)));

        [JsonIgnore]
        public bool IsEmpty
            => !Categories.Any(x => x.Products.Any());
    }

    public class ProductSkuName
    {
        [JsonPropertyName("sku")]
        public string Sku { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class ProductSkuNames
    {
        [JsonPropertyName("sku")]
        public string Sku { get; set; }

        [JsonPropertyName("names")]
        public IEnumerable<string> Names { get; set; }

        [JsonIgnore]
        public int ItemsPerPack
        {
            get
            {
                if (Names == null || !Names.Any())
                    return 1;

                MatchCollection matches = null;

                foreach (var name in Names) {
                    matches = Regex.Matches(name, "\\d+\\s*(ea|шт|gb)");
                    foreach (Match match in matches) {
                        var numberMatches = Regex.Matches(match.Value, "\\d+");
                        if (numberMatches.Any())
                            return Convert.ToInt32(numberMatches.First().Value);
                    }
                }

                return 1;
            }
        }

        public override string ToString()
            => $"{Sku} {(Names.Any() ? Names.First() : string.Empty)}".Trim();
    }
}