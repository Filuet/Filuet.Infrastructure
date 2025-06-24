using Filuet.Infrastructure.Abstractions.Business;
using Filuet.Infrastructure.Abstractions.Business.Models;
using Filuet.Infrastructure.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace Test
{
    public class CartTest
    {
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCartDiff), MemberType = typeof(TestDataGenerator))]
        public void Test_Cart_difference(Cart a, Cart b, IEnumerable<CartItem> missing, IEnumerable<CartItem> redundant)
        {
            // Prepare
            IEnumerable<CartItem> missingFact = null;
            IEnumerable<CartItem> redundantFact = null;

            // Pre-validate
            Assert.NotNull(a);
            Assert.NotNull(b);

            // Perform
            (missingFact, redundantFact) = a - b;

            // Post-validate
            Assert.Equal(Cart.Create(missing), Cart.Create(missingFact));
            Assert.Equal(Cart.Create(redundant), Cart.Create(redundantFact));
        }

        [Fact]
        public void Test_Cart_additional_params() {
            // Prepare
            Cart cart = new Cart();
            cart.AdditionalParams.Add("date", DateTime.UtcNow);

            // Pre-validate
            Assert.NotNull(cart);

            // Perform
            DateTime date = cart.GetParam<DateTime>("date");

            // Post-validate
        }

        [Fact]
        public void Test_Cart_add_additional_param() {
            // Prepare
            Cart cart = new Cart();

            // Pre-validate
            Assert.NotNull(cart);

            // Perform
            cart.AddParam("date", DateTime.UtcNow);

            // Post-validate
        }

        [Theory]
        [InlineData("{\"items\":[{\"sku\":\"4467\",\"qty\":1}],\"additionalParams\":{\"additionalProp1\":\"string\",\"additionalProp2\":\"string\",\"additionalProp3\":\"string\"}}")]
        public void Test_Cart_deserialization(string payload) {
            Cart cart = JsonSerializer.Deserialize<Cart>(payload);
        }

        [Fact]
        public void Test_CostCalculationTotal_serialization() {
            // Prepare
            CostCalculationTotal cartCalc = new CostCalculationTotal {
                Items = [new CostCalculation{ Sku ="4466", Total = Money.Create(1, Currency.Euro)}]
            };

            // Pre-validate
            Assert.NotNull(cartCalc);

            // Perform
            string result = JsonSerializer.Serialize(cartCalc);

            // Post-validate
        }
    }
}