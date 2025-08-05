using System;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Models
{
    public class Period
    {
        [JsonPropertyName("from")]
        public DateTimeOffset From { get; set; }
        [JsonPropertyName("to")]
        public DateTimeOffset To { get; set; }
    }
}
