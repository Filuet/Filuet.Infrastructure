using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Models
{
    public struct GeoCoordinate
    {
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        public override string ToString() => string.Format("{0},{1}", Latitude, Longitude);
    }
}
