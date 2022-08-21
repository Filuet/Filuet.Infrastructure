using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Models
{
    public struct GeoCoordinate
    {
        private readonly double latitude;
        private readonly double longitude;

        [JsonPropertyName("lat")]
        public double Latitude => latitude;

        [JsonPropertyName("lon")]
        public double Longitude => longitude;

        public GeoCoordinate(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public override string ToString() => string.Format("{0},{1}", Latitude, Longitude);
    }
}
