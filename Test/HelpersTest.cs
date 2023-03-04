using Filuet.Infrastructure.Abstractions.Helpers;
using Xunit;

namespace Test
{
    public class HelpersTest
    {
        private enum foo { a, b, c }

        [Fact]
        public void Test_Value_in_enum()
        {
            // Prepare
            foo x = foo.a;

            // Pre-validate


            // Perform
            bool belongs = x.In(foo.a, foo.c);

            // Post-validate
            Assert.True(belongs);
        }

        [Fact]
        public void Test_Value_in_enum_nullable()
        {
            // Prepare
            foo? x = null;

            // Pre-validate


            // Perform
            bool belongs = x.In(foo.a, foo.c);

            // Post-validate
            Assert.True(belongs);
        }
    }
}
