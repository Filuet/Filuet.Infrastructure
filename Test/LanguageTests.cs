using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using Xunit;

namespace Test
{
    public class LanguageTests
    {
        [Theory]
        [InlineData("закажи его и ещё 5 NRG", false)]
        [InlineData("cjk sdfsf dlk", true)]
        [InlineData("cjk sdщsf dlk", false)]
        public void Test_IsInEnglish(string input, bool result) {
            // Perform
            bool fact = input.IsLatinText();

            // Post-validate
            Assert.Equal(fact, result);
        }

        [Theory]
        [InlineData("38008946", null, null)]
        [InlineData("igor.gutsaev@filuet.com", null, null)]
        [InlineData("закажи его и ещё 5 NRG", Language.Russian, Country.Russia)]
        [InlineData("order Aloe", Language.English, null)]
        [InlineData("შეუკვეთე ალოე", null, null)] // GetLanguage function don't know Georgian yet 
        [InlineData("закажи 2x NRG и Roseguard", Language.Russian, null)]
        [InlineData("Պատվիրեք 1 Roseguard", Language.Armenian, Country.Armenia)]
        [InlineData("doties uz grozu", Language.Latvian, Country.Latvia)]
        [InlineData("soat nechi bo'ldi?", Language.Uzbek, Country.Uzbekistan)]
        [InlineData("2 ta NRG buyurtma bering", Language.Uzbek, Country.Uzbekistan)]
        public void Test_GetLanguage(string input, Language? actual, Country? country) {
            // Perform
            Language? fact = input.GetLanguage(country);

            // Post-validate
            Assert.Equal(fact, actual);
        }

        [Theory]
        //[InlineData("ЕЛЕНА ХОЛОХОН", "ELENA KHOLOKHON")]
        //[InlineData("Елена Холохон", "Elena Kholokhon")]
        [InlineData("Мирзо Улуғбек", "Mirzo Ulugbek")]
        [InlineData("Муҳаммад", "Muhammad")]
        public void Test_Latinize(string source, string expected) {
            string actual = source.Latinize();
            Assert.Equal(expected, actual);
        }
    }
}
