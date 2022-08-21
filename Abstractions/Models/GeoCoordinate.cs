using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Models
{
    [ComplexType]
    public class GeoCoordinate
    {
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        public override string ToString() => string.Format("{0},{1}", Latitude, Longitude);
    }
}
