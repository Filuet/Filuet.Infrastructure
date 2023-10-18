using Filuet.Infrastructure.Abstractions.Business;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Formats.Tar;
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

        [Theory]
        [InlineData("1,233.00 ₹", 1233, Currency.IndianRupee)]
        [InlineData("1233.00 ₹", 1233, Currency.IndianRupee)]
        public void Test_Parse_money(string input, decimal valueExpected, Currency currencyExpected) {
            // Prepare
            Money expected = Money.Create(valueExpected, currencyExpected);

            // Pre-validate


            // Perform
            Money actual = Money.Parse(input);

            // Post-validate
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1233, 154, 1387, Currency.IndianRupee)]
        public void Test_Parse_summ(decimal a, decimal b, decimal c, Currency currencyExpected) {
            // Prepare
            Money _a = Money.Create(a, currencyExpected);
            Money _b = Money.Create(b, currencyExpected);
            Money expected = Money.Create(c, currencyExpected);

            // Pre-validate


            // Perform
            Money actual = _a + _b;

            // Post-validate
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1233, 154, 1387, Currency.IndianRupee)]
        public void Test_Parse_linq_summ(decimal a, decimal b, decimal c, Currency currencyExpected) {
            // Prepare
            Money _a = Money.Create(a, currencyExpected);
            Money _b = Money.Create(b, currencyExpected);
            IEnumerable<Money> terms = new Money[] { _a, };
            Money expected = Money.Create(c, currencyExpected);

            // Pre-validate


            // Perform
            Money actual = terms.Sum();

            // Post-validate
            Assert.Equal(expected, actual);
        }
    }
}
