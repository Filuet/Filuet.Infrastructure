using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using Filuet.Infrastructure.Ordering.Dto;

namespace Filuet.Infrastructure.Ordering.Converters
{
    public class OrderRollbackReasonConverter : JsonConverter<OrderRollbackReason?>
    {
        public override OrderRollbackReason? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var data = reader.GetString();
            if (string.IsNullOrWhiteSpace(data))
                return null;

            return EnumHelpers.GetValueFromCode<OrderRollbackReason>(data);
        }

        public override void Write(Utf8JsonWriter writer, OrderRollbackReason? reason, JsonSerializerOptions options)
        {
            writer.WriteStringValue(reason?.GetCode());
        }
    }
}
