using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class DeliveryDetails : ShipBaseDetails
    {
        [JsonPropertyName("address")]
        public Address Address { get; set; } = new Address();
        [JsonIgnore]
        public bool IsSufficient
            => Address != null && Address.IsSufficient && !string.IsNullOrWhiteSpace(MobileNumber) && !string.IsNullOrWhiteSpace(Recipient) && MobileNumber.Trim().Length >= 8 && Recipient.Trim().Length >= 3;

        public override string ToString()
            => $"{Address} {MobileNumber}";
    }
}