using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public class AscKioskModeConverter : JsonConverter<AscKioskMode>
    {
        public override AscKioskMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => EnumHelpers.GetValueFromCode<AscKioskMode>(reader.GetString());

        public override void Write(Utf8JsonWriter writer, AscKioskMode mode, JsonSerializerOptions options)
            => writer.WriteStringValue(EnumHelpers.GetCode(mode));
    }
}