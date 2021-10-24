using System.Text.RegularExpressions;

namespace Filuet.Infrastructure.Abstractions.Helpers
{
    public static class StringHelpers
    {
        public static bool IsGuid(this string source)
        {
            source = source.Trim().Replace(" ", "");

            if (string.IsNullOrWhiteSpace(source) || source.Length < 32)
                return false;

            Regex regex = new Regex(@"^[{(]?[0-9A-F]{8}[-]?([0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$", RegexOptions.IgnoreCase);
            return regex.Match(source).Success;
        }
    }
}