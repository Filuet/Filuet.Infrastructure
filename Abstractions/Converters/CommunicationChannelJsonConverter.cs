using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public class CommunicationChannelJsonConverter : JsonConverter<CommunicationChannel>
    {
        public override CommunicationChannel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return EnumHelpers.GetValueFromCode<CommunicationChannel>(value);
        }

        public override void Write(Utf8JsonWriter writer, CommunicationChannel language, JsonSerializerOptions options) =>
            writer.WriteStringValue(EnumHelpers.GetCode(language));
    }
}