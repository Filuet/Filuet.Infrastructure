using Filuet.Infrastructure.Communication.Helpers;
using Xunit;

namespace Test
{
    public class HookHelpersTest
    {
        [Theory]
        [InlineData("`| x`n8bX1WM.k4f3", "foo")]
        public void Test_encrypt(string secret, string data)
        {
            string result = HookHelpers.Encrypt(secret, data);
        }
    }
}
