using System.Text.RegularExpressions;

namespace Filuet.Infrastructure.Abstractions.Helpers
{
    public static class StringHelpers
    {
        public static bool IsMacAddress(this string macAddress)
            => CheckMatch(macAddress, "^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$");

        public static bool IsGuid(this string source)
        {
            source = source.Trim().Replace(" ", "");

            if (string.IsNullOrWhiteSpace(source) || source.Length < 32)
                return false;

            return CheckMatch(source, @"^[{(]?[0-9A-F]{8}[-]?([0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$");
        }

        public static bool IsEmail(this string input)
            => CheckMatch(input, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public static bool IsPhone(this string mobile)
            => CheckMatch(mobile, @"^(\+\d{1,2}\s)\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$");

        public static bool IsValidJson(string json)
        {
            try
            {
                System.Text.Json.JsonDocument.Parse(json);
                return true;
            }
            catch { }

            return false;
        }

        private static bool CheckMatch(string source, string regularExpression)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(regularExpression))
                return false;

            Regex regex = new Regex(regularExpression, RegexOptions.IgnoreCase);
            return regex.Match(source).Success;
        }
    }
}