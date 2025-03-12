using Filuet.Infrastructure.Abstractions.Enums;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class ContactReference
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("channel")]
        public CommunicationChannel Channel { get; set; }
    }
}