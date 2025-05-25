using Filuet.Infrastructure.Abstractions.Business.Models;
using System;
using System.Collections.Generic;
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
    }
}