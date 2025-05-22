using Filuet.Infrastructure.Abstractions.Business.Models;
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
    }
}