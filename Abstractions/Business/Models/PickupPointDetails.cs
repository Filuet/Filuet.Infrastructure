using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class PickupPointDetails : ShipBaseDetails
    {
        [JsonPropertyName("point")]
        public PickupPoint Point { get; set; }

        [JsonIgnore]
        public bool IsSufficient
            => Point != null && Point.IsSufficient && !string.IsNullOrWhiteSpace(MobileNumber) && !string.IsNullOrWhiteSpace(Recipient) && MobileNumber.Trim().Length >= 8 && Recipient.Trim().Length >= 3;

        public override string ToString()
            => $"{Point} {MobileNumber} {Recipient}";
    }
}