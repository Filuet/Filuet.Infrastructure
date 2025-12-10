using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class LockerDetails : ShipBaseDetails
    {
        [JsonPropertyName("locker")]
        public ParcelLocker Locker { get; set; }

        [JsonIgnore]
        public bool IsSufficient
            => Locker != null && Locker.IsSufficient && !string.IsNullOrWhiteSpace(MobileNumber) && !string.IsNullOrWhiteSpace(Recipient) && MobileNumber.Trim().Length >= 8 && Recipient.Trim().Length >= 3;

        public override string ToString()
            => $"{Locker} {MobileNumber} {Recipient}";
    }
}