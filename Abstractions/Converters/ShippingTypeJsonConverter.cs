using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public class ShippingTypeJsonConverter : JsonConverter<ShippingType>
    {
        public override ShippingType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => EnumHelpers.GetValueFromCode<ShippingType>(reader.GetString());

        public override void Write(Utf8JsonWriter writer, ShippingType status, JsonSerializerOptions options)
            => writer.WriteStringValue(status.GetCode());
    }
}