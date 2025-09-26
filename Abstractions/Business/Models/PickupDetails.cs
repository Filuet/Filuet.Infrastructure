using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class PickupDetails {
        [JsonPropertyName("storeCode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string StoreCode { get; set; }
        /// <summary>
        /// Recipient name
        /// </summary>
        [JsonPropertyName("recipient")]
        public string Recipient { get; set; }
        [JsonPropertyName("mobileNumber")]
        public string MobileNumber { get; set; }

        [JsonIgnore]
        public bool IsSufficient
            => !string.IsNullOrWhiteSpace(MobileNumber) && !string.IsNullOrWhiteSpace(Recipient) && MobileNumber.Trim().Length >= 8 && Recipient.Trim().Length >= 3;

        public override string ToString()
            => $"{Recipient} {MobileNumber}";
    }
}