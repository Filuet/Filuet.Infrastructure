using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public class OrderStatusJsonConverter : JsonConverter<OrderStatus>
    {
        public override OrderStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => EnumHelpers.GetValueFromCode<OrderStatus>(reader.GetString());

        public override void Write(Utf8JsonWriter writer, OrderStatus status, JsonSerializerOptions options)
            => writer.WriteStringValue(status.GetCode());
    }
}