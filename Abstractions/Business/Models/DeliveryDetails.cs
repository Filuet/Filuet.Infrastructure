using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class DeliveryDetails
    {
        [JsonPropertyName("address")]
        public Address Address { get; set; }
        [JsonPropertyName("mobileNumber")]
        public string MobileNumber { get; set; }
        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        public override string ToString()
            => $"{Address} {MobileNumber} {Comment}";
    }
}
