using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Ordering.Converters
{
    public class JsonSerializationOptions
    {
        public static JsonSerializerOptions EventPrettyOptions
        {
            get
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                options.Converters.Add(new JsonStringEnumConverter());

                return options;
            }
        }
    }
}