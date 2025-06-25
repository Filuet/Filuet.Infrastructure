using Filuet.Infrastructure.Abstractions.Enums;
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

        [Theory]
        [InlineData("foo", "acbd18db4cc2f85cedef654fccc4a4d8")]
        public void Test_Md5(string input, string expected) {
            // Perform
            string actual = input.CalculateMd5Hash();

            // Post-validate
            Assert.Equal(expected, actual);
        }        
        
        [Theory]
        [InlineData("e.ragone@prc.kedai.lm.lt", true)]
        public void Test_IsEmail(string input, bool fact) {
            // Perform
            bool expected = input.IsEmail();

            // Post-validate
            Assert.Equal(expected, fact);
        }

        [Theory]
        [InlineData("<p>h</p>e<br>l<br />l<br/>o</br>, </ br>Wo<div>r<img source=\"foo.png\">l</html>d!", "hello, World!")]
        public void Test_Remove_html_tags(string input, string expected) {
            // Perform
            string fact = input.RemoveHtmlTags();

            // Post-validate
            Assert.Equal(expected, fact);
        }
    }
}