using Filuet.Infrastructure.Abstractions.Enums;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class CategoryLocalization
    {
        [JsonPropertyName("lang")]
        public Language Language { get; set; }
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Name { get; set; }
    }
}