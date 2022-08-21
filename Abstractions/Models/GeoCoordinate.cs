using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Models
{
    [ComplexType]
    public class GeoCoordinate
    {
        [JsonPropertyName("lat")]
        public decimal Latitude { get; set; }

        [JsonPropertyName("lon")]
        public decimal Longitude { get; set; }

        public override string ToString() => string.Format("{0},{1}", Latitude, Longitude);
    }
}
