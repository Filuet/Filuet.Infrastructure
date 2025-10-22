using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class PickupDetails : ShipBaseDetails
    {
        [JsonPropertyName("storeCode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string StoreCode { get; set; }
        [JsonIgnore]
        public bool IsSufficient
            => !string.IsNullOrWhiteSpace(MobileNumber) && !string.IsNullOrWhiteSpace(Recipient) && MobileNumber.Trim().Length >= 8 && Recipient.Trim().Length >= 3;

        public override string ToString()
            => $"{Recipient} {MobileNumber}";
    }
}