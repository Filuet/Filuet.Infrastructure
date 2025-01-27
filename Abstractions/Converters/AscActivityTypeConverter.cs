using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public class AscActivityTypeConverter : JsonConverter<AscActivityType>
    {
        public override AscActivityType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => EnumHelpers.GetValueFromCode<AscActivityType>(reader.GetString());

        public override void Write(Utf8JsonWriter writer, AscActivityType activity, JsonSerializerOptions options)
            => writer.WriteStringValue(EnumHelpers.GetCode(activity));
    }
}