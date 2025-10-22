using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public abstract class ShipBaseDetails
    {
        /// <summary>
        /// Recipient name
        /// </summary>
        [JsonPropertyName("recipient")]
        public string Recipient { get; set; }
        [JsonPropertyName("mobileNumber")]
        public string MobileNumber { get; set; }
        /// <summary>
        /// Deliver paper invoice with the parcel
        /// </summary>
        [JsonPropertyName("invoice")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Invoice { get; set; }
    }
}