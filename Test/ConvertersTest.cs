using Filuet.Infrastructure.Abstractions.Converters;
using System.Text.Json;
using Xunit;

namespace Test
{
    public class ConvertersTest
    {
        [Fact]
        public void Test_N2JsonConverter()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new N2JsonConverter());

            string d = JsonSerializer.Serialize(new { a = 4.012m }, options);
        }
    }
}
