using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public class IntervalJsonConverter : JsonConverter<Interval>
    {
        public override Interval Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return EnumHelpers.GetValueFromCode<Interval>(value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Interval inverval,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(EnumHelpers.GetCode(inverval));
    }
}