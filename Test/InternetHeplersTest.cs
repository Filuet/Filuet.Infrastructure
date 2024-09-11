using Filuet.Infrastructure.Abstractions.Business;
using Filuet.Infrastructure.Communication.Helpers;
using System.Collections.Generic;
using Xunit;

namespace Test
{
    public class InternetHeplersTest
    {
        [Theory]
        [InlineData("188.227.90.1", true)]
        [InlineData("https://sdjkiuh34312uiadf.pl", false)]
        public void Test_Parse_linq_summ(string address, bool expected) {
            // Prepare

            // Pre-validate

            // Perform
            bool actual = address.PingHost();

            // Post-validate
            Assert.Equal(expected, actual);
        }
    }
}
