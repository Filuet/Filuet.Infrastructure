using Filuet.Infrastructure.Abstractions.Business.Models;
using Filuet.Infrastructure.Abstractions.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class CatalogTest
    {
        [Theory]
        [InlineData("{\"categories\":[{\"loc\":[{\"lang\":\"lv\",\"name\":\"foo\"}],\"products\":[{\"sku\":\"0105\",\"weight\":84,\"loc\":[{\"lang\":\"lv\",\"name\":\"bar\",\"desc\":\"baz\"}],\"price\":{\"value\":18.65,\"curr\":\"EUR\"},\"additionalParams\":{\"rewardPoints\":\"19.95\",\"maxQty\":\"0\"}}]}]}")]
        public void Test_Deserialize(string catalog) {
            // Prepare

            // Pre-validate
            Assert.NotNull(catalog);

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new LanguageJsonConverter());

            // Perform
            Catalog result = JsonSerializer.Deserialize<Catalog>(catalog, options);

            // Post-validate

        }
    }
}
