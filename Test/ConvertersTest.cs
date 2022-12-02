using Filuet.Infrastructure.Abstractions.Converters;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
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

        [Theory]
        [InlineData("RR")]
        public void Test_EnumHelpers_Try_GetCountry_By_Invalid_code(string code)
        {
            // Prepare
            Country? expected = 0;

            // Pre-validate
            Assert.True(code.Length == 2);

            // Perform
            bool result = EnumHelpers.TryGetValueFromCode(code, out Country location);

            // Validate
            Assert.False(result);
            Assert.Equal(expected, location);
        }
    }
}
