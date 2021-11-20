using Filuet.Infrastructure.Abstractions.Helpers;
using Filuet.Infrastructure.Ordering.Enums;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Ordering.Converters
{
    public class GoodsObtainingMethodConverter : JsonConverter<GoodsObtainingMethod>
    {
        public override GoodsObtainingMethod Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => EnumHelpers.GetValueFromCode<GoodsObtainingMethod>(reader.GetString());

        public override void Write(Utf8JsonWriter writer, GoodsObtainingMethod obtaining, JsonSerializerOptions options)
            => writer.WriteStringValue(obtaining.GetCode());
    }
}