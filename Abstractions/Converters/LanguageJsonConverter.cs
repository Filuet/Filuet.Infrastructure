using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public class LanguageJsonConverter : JsonConverter<Language>
    {
        public override Language Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return EnumHelpers.GetValueFromCode<Language>(value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Language language,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(EnumHelpers.GetCode(language));
    }
}