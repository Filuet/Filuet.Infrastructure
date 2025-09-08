using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

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

    public class ProductSkuNames {
        [JsonPropertyName("sku")]
        public string Sku { get; set; }

        [JsonPropertyName("names")]
        public IEnumerable<string> Names { get; set; }
    }
}