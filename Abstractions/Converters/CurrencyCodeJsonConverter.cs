using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public class CurrencyCodeJsonConverter : JsonConverter<Currency>
    {
        public override Currency Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => EnumHelpers.GetValueFromCode<Currency>(reader.GetString());

        public override void Write(Utf8JsonWriter writer, Currency currency, JsonSerializerOptions options)
            => writer.WriteStringValue(currency.GetCode());
    }
}