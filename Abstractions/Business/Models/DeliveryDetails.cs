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
        /// <summary>
        /// Deliver paper invoice with the parcel
        /// </summary>
        [JsonPropertyName("invoice")]
        public bool Invoice { get; set; }

        public bool IsSufficient
            => Address.IsSufficient && !string.IsNullOrWhiteSpace(MobileNumber) && MobileNumber.Trim().Length >= 8;

        public override string ToString()
            => $"{Address} {MobileNumber} {Comment}";
    }
}