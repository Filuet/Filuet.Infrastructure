using Filuet.Infrastructure.Abstractions.Business;
using Filuet.Infrastructure.Abstractions.Enums;
using Xunit;

namespace Test
{
    public class MoneyTest
    {
        [Theory]
        [InlineData(1233.043, Currency.IndianRupee, "1,233.04 ₹")]
        [InlineData(1233, Currency.IndianRupee, "1,233.00 ₹")]
        public void Test_Ping_ES_Plus_Belt(decimal value, Currency currency, string expected)
        {
            // Prepare
            Money money = Money.Create(value, currency);

            // Pre-validate


            // Perform
            string actual = money.ToString(true);

            // Post-validate
            Assert.Equal(expected, actual);
        }
    }
}
