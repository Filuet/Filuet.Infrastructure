using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public abstract class BaseJsonEnumCodeConverter<T> : JsonConverter<T> where T: Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => EnumHelpers.GetValueFromCode<T>(reader.GetString());

        public override void Write(Utf8JsonWriter writer, T val, JsonSerializerOptions options)
            => writer.WriteStringValue(val.GetCode());
    }
}