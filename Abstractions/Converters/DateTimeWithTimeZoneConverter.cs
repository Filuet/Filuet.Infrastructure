using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Converters
{
    public class DateTimeWithTimeZoneConverter : JsonConverter<DateTime>
    {
        private TimeZoneInfo pacificZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();

            if (value == null)
                return DateTime.MinValue;

            var pacificTime = DateTime.Parse(value);
            return TimeZoneInfo.ConvertTimeToUtc(pacificTime, pacificZone);
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTime date,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(date.ToString("yyyy-MM-ddTHH:mm:sszzz"));
    }
}
