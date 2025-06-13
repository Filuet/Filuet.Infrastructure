using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class CartCheckResult
    {
        [JsonPropertyName("unavailable")]
        public IEnumerable<SkuCheckResult> Unavailable { get; set; }

        public CartCheckResult(IEnumerable<SkuCheckResult> unavailable) {
            Unavailable = unavailable;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            foreach (var x in Unavailable)
                sb.AppendLine($"{x.Sku}: {x.Error}");

            return sb.ToString();
        }
    }
}