using Filuet.Infrastructure.Abstractions.Enums;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class Address
    {
        [JsonPropertyName("postCode")]
        public string PostCode { get; set; }
        [JsonPropertyName("country")]
        public Country Country { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("addressLine")]
        /// <summary>
        /// Victorenko st, 14-23
        /// </summary>
        public string AddressLine { get; set; }

        public bool IsSufficient
            => !string.IsNullOrWhiteSpace(PostCode) && PostCode.Trim().Length >= 4 &&
            !string.IsNullOrWhiteSpace(City) && City.Trim().Length >= 2 &&
            !string.IsNullOrWhiteSpace(AddressLine) && AddressLine.Trim().Length >= 4;

        public override string ToString()
            => $"{PostCode} {Country} {City} {AddressLine}".Trim();
    }
}