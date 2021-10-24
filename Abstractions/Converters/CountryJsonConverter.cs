using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public class CountryJsonConverter : JsonConverter<Country>
    {
        public override Country Read(
                    ref Utf8JsonReader reader,
                    Type typeToConvert,
                    JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return EnumHelpers.GetValueFromCode<Country>(value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Country country,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(EnumHelpers.GetCode(country));
    }
}