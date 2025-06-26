using Filuet.Infrastructure.Abstractions.Business;
using Filuet.Infrastructure.Abstractions.Business.Models;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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
            cart.AdditionalParams.Add("date", DateTime.UtcNow.ToString());
            cart.AdditionalParams.Add("currency", Currency.ArmenianDram.GetCode());

            // Pre-validate
            Assert.NotNull(cart);

            // Perform
            DateTime date = Convert.ToDateTime(cart.GetParam("date"));
            Currency curr = EnumHelpers.GetValueFromCode<Currency>(cart.GetParam("currency"));

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

        [Fact]
        public void Test_Cart_Add_item() {
            // Prepare
            Cart cart = Cart.Create([CartItem.Create("0105", 1)]);

            // Pre-validate
            Assert.NotNull(cart);

            // Perform
            cart += CartItem.Create("4466", 1);

            // Post-validate
            Assert.Equal(cart.Items.Count(), 2);
            Assert.Equal(cart.Items.Last().Quantity, 1);
        }

        [Fact]
        public void Test_Cart_Increase_quantity() {
            // Prepare
            Cart cart = Cart.Create([CartItem.Create("0105", 1)]);

            // Pre-validate
            Assert.NotNull(cart);

            // Perform
            cart += CartItem.Create("0105", 1);

            // Post-validate
            Assert.Equal(cart.Items.Count(), 1);
            Assert.Equal(cart.Items.First().Quantity, 2);
        }

        [Fact]
        public void Test_Cart_Decrease_quantity() {
            // Prepare
            Cart cart = Cart.Create([CartItem.Create("0105", 5)]);

            // Pre-validate
            Assert.NotNull(cart);

            // Perform
            cart -= CartItem.Create("0105", 3);

            // Post-validate
            Assert.Equal(cart.Items.Count(), 1);
            Assert.Equal(cart.Items.First().Quantity, 2);
        }

        [Fact]
        public void Test_Cart_Decrease_quantity_to_empty() {
            // Prepare
            Cart cart = Cart.Create([CartItem.Create("0105", 5)]);

            // Pre-validate
            Assert.NotNull(cart);

            // Perform
            cart -= CartItem.Create("0105", 5);

            // Post-validate
            Assert.Equal(cart.Items.Count(), 0);
        }
    }
}