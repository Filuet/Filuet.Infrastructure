using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Filuet.Infrastructure.Ordering.Converters
{
    public class PaymentMethodNullableConverter : JsonConverter<PaymentMethod?>
    {
        public override PaymentMethod? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var data = reader.GetString();
            if (string.IsNullOrWhiteSpace(data))
                return null;

            return EnumHelpers.GetValueFromCode<PaymentMethod>(data);
        }

        public override void Write(Utf8JsonWriter writer, PaymentMethod? method, JsonSerializerOptions options)
        {
            writer.WriteStringValue(method?.GetCode());
        }
    }
}
