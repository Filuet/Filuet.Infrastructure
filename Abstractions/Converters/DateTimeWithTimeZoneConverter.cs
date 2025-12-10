using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public class DateTimeWithTimeZoneConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();

            if (value == null)
                return DateTime.MinValue;

            return DateTime.Parse(value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTime date,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(date.ToString("yyyy-MM-ddTHH:mm:sszzz"));
    }
}
