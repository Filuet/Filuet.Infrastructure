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
        [InlineData("")]
        [InlineData("lt")]
        public void Test_Enum_From_Code_Nullable(string code)
        {
            Country? res = EnumHelpers.GetValueFromCode1<Country>(code);
        }
    }
}
