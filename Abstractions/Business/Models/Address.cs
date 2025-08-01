﻿using Filuet.Infrastructure.Abstractions.Enums;
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

        public override string ToString()
            => $"{PostCode} {Country} {City} {AddressLine}".Trim();
    }
}
