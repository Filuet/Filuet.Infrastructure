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
}