using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System.Linq;
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
        [InlineData("38008946", null, Country.Latvia)]
        [InlineData("igor.gutsaev@filuet.com", null, Country.Latvia)]
        [InlineData("закажи его и ещё 5 NRG", new Language[] { Language.Russian, Language.English }, Country.Russia)]
        [InlineData("order Aloe", new Language[] { Language.Uzbek, Language.English }, Country.Uzbekistan)]
        [InlineData("შეუკვეთე ალოე", null, Country.Georgia)] // GetLanguage function don't know Georgian yet 
        [InlineData("закажи 2x NRG и Roseguard", new Language[] { Language.Russian, Language.English, Language.Uzbek }, Country.Uzbekistan)]
        [InlineData("Պատվիրեք 1 Roseguard", new Language[] { Language.Armenian, Language.English }, Country.Armenia)]
        [InlineData("doties uz grozu", new Language[] { Language.Latvian }, Country.Latvia)]
        [InlineData("soat nechi bo'ldi?", new Language[] { Language.Uzbek, Language.English }, Country.Uzbekistan)]
        [InlineData("2 ta NRG buyurtma bering", new Language[] { Language.Uzbek, Language.English }, Country.Uzbekistan)]
        public void Test_GetLanguage(string input, Language[] actual, Country country) {
            // Perform
            Language[] fact = input.GetLanguages(country);

            // Post-validate
            Assert.Equal(fact?.OrderBy(x => x), actual?.OrderBy(x => x));
        }

        [Theory]
        [InlineData("ЕЛЕНА ХОЛОХОН", "ELENA KHOLOKHON")]
        [InlineData("Елена Холохон", "Elena Kholokhon")]
        [InlineData("Мирзо Улуғбек", "Mirzo Ulugbek")]
        [InlineData("Муҳаммад", "Muhammad")]
        public void Test_Latinize(string source, string expected) {
            string actual = source.Latinize();
            Assert.Equal(expected, actual);
        }
    }
}
