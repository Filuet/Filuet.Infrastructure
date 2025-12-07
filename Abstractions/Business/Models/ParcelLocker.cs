using Filuet.Infrastructure.Abstractions.Enums;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class ParcelLocker
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("country")]
        public Country Country { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("freightCode")]
        public string FreightCode { get; set; }
        [JsonPropertyName("warehouseCode")]
        public string WarehouseCode { get; set; }
        [JsonPropertyName("serviceName")]
        public string ServiceName { get; set; }
        public override string ToString()
            => $"{Address} ({ServiceName} - #{Code})";
    }
}