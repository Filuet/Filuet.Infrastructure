using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class DeliveryDetails
    {
        [JsonPropertyName("address")]
        public Address Address { get; set; } = new Address();
        /// <summary>
        /// Recipient name
        /// </summary>
        [JsonPropertyName("recipient")]
        public string Recipient { get; set; }
        [JsonPropertyName("mobileNumber")]
        public string MobileNumber { get; set; }
        [JsonPropertyName("comment")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Comment { get; set; }
        /// <summary>
        /// Deliver paper invoice with the parcel
        /// </summary>
        [JsonPropertyName("invoice")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Invoice { get; set; }
        [JsonIgnore]
        public bool IsSufficient
            => Address != null && Address.IsSufficient && !string.IsNullOrWhiteSpace(MobileNumber) && !string.IsNullOrWhiteSpace(Recipient) && MobileNumber.Trim().Length >= 8 && Recipient.Trim().Length >= 3;

        public override string ToString()
            => $"{Address} {MobileNumber} {Comment}";
    }
}