using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public class BotChannelJsonConverter : JsonConverter<BotChannel>
    {
        public override BotChannel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return EnumHelpers.GetValueFromCode<BotChannel>(value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BotChannel channel,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(EnumHelpers.GetCode(channel));
    }
}