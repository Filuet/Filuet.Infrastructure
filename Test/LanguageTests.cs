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
            bool fact = input.IsInEnglish();

            // Post-validate
            Assert.Equal(fact, result);
        }

        [Theory]
        [InlineData("38008946", null)]
        [InlineData("igor.gutsaev@filuet.com", null)]
        [InlineData("закажи его и ещё 5 NRG", Language.Russian)]
        [InlineData("order Aloe", Language.English)]
        [InlineData("შეუკვეთე ალოე", null)] // GetLanguage function don't know Georgian yet 
        [InlineData("закажи 2x NRG и Roseguard", Language.Russian)]
        [InlineData("Պատվիրեք 1 Roseguard", Language.Armenian)]
        [InlineData("doties uz grozu", Language.Latvian)]
        public void Test_GetLanguage(string input, Language? actual) {
            // Perform
            Language? fact = input.GetLanguage();

            // Post-validate
            Assert.Equal(fact, actual);
        }
    }
}
