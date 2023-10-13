using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public class TimeOfDayJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return new DateTime(1, 1, 1, Convert.ToInt32(value.Substring(0,2)), Convert.ToInt32(value.Substring(3, 2)), Convert.ToInt32(value.Substring(6, 2)));
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTime date,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(date.ToString("HH:mm:ss"));
    }
}