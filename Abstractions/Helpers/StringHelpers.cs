using Filuet.Infrastructure.Abstractions.Business.Models;
using Filuet.Infrastructure.Abstractions.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Filuet.Infrastructure.Abstractions.Helpers
{
    public static class StringHelpers
    {
        public static string GetPaymentSystem(this string cardNumber) {
            Regex regexVI = new Regex(@"^4");
            Regex regexMC = new Regex(@"^(5[1-5]|(?:222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720))");
            Regex regexJCB = new Regex(@"^(31|35)");

            if (regexVI.IsMatch(cardNumber))
                return @"VI";
            else if (regexMC.IsMatch(cardNumber))
                return @"MC";
            else if (regexJCB.IsMatch(cardNumber))
                return @"JCB";

            return @"VI";
        }

        public static bool IsMacAddress(this string macAddress)
                => CheckMatch(macAddress, "^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$");

        public static bool IsGuid(this string source) {
            if (source == null)
                return false;

            source = source.Trim().Replace(" ", "");

            if (string.IsNullOrWhiteSpace(source) || source.Length < 32)
                return false;

            return CheckMatch(source, @"^[{(]?[0-9A-F]{8}[-]?([0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$");
        }

        public static bool IsEmail(this string input)
            => CheckMatch(input, @"^([\w\.\-]+)@([\w\-.]+)((\.(\w){2,3})+)$");

        public static bool IsPhone(this string mobile)
            => CheckMatch(mobile, @"^(\+\d{1,2}\s)\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$");

        public static bool IsInEnglish(this string input)
            => CheckMatch(input, @"^[a-zA-Z0-9 !""№;%:?@*()#_\-\\\/|+=.,<>'`~]*$");

        public static Language? GetLanguage(this string input) {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            // input contains only English, digits and special symbols
            if (input.IsInEnglish())
                return Language.English;

            // if Russian letters only, digits and special symbols
            if (CheckMatch(input, @"^[а-яА-Я0-9 !""№;%:?@*()#_\-\\\/|+=.,<>'`~]*$"))
                return Language.Russian;

            // if Armenian letters only, digits and special symbols
            if (CheckMatch(input, @"^[ա-ֆԱ-Ֆ0-9 !""№;%:?@*()#_\-\\\/|+=.,<>'`~]*$"))
                return Language.Armenian;

            #region mixed text detected. Let's analyze it more thoroughly
            var charsToRemove = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                " ", "!", "\"", "№", ";", "%", ":", "?", "@", "*", "(", ")", "#", "_", "\\", "-", "/", "|", "+", "=", ".", ",", "<", ">", "'", "`", "~", "*" };

            foreach (var c in charsToRemove)
                input = input.Replace(c, string.Empty);

            List<char> listLatin = new List<char>();
            for (char c = 'a'; c <= 'z'; ++c)
                listLatin.Add(c);

            List<char> listCyrillic = new List<char>();
            for (char c = 'а'; c <= 'я'; ++c)
                listCyrillic.Add(c);

            List<char> listArmenian = new List<char>();
            for (char c = 'ա'; c <= 'ֆ'; ++c)
                listArmenian.Add(c);

            input = input.Trim().ToLower();

            // (Language, Letters count) dictionary
            Dictionary<Language, int> langLettersCount = Enum.GetValues<Language>().ToDictionary(x => x, y => 0);

            langLettersCount[Language.English] = input.Count(x => listLatin.Contains(x)); // how many latin letters in the text
            langLettersCount[Language.Russian] = input.Count(x => listCyrillic.Contains(x)); // how many cyrillic... 
            langLettersCount[Language.Armenian] = input.Count(x => listArmenian.Contains(x)); // ...armenian...

            // if the test doesn't contain one of the provided by this function languages
            if (!langLettersCount.Any() || langLettersCount.Where(x => x.Value > 0).Count() == 0)
                return null;

            // if the text is in only one languages- that's it
            if (langLettersCount.Where(x => x.Value > 0).Count() == 1)
                return langLettersCount.First(x => x.Value > 0).Key;

            // if the text is in only two languages- one specific and English, we can state that it is a text in the first one
            if (langLettersCount.Where(x => x.Value > 0).Count() == 2 && langLettersCount.Where(x => x.Value > 0).Any(x => x.Key == Language.English))
                return langLettersCount.First(x => x.Value > 0 && x.Key != Language.English).Key;

            // 2 or more languages (not English) detected. let's dive deeper
            return langLettersCount.OrderByDescending(x => x.Value).First().Key;
            #endregion
        }

        public static string CalculateMd5Hash(this string input) {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create()) {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower();
            }
        }

        public static bool IsNowWorkingHours(this string schedule) {
            if (!schedule.IsValidJson())
                throw new ArgumentException("Invalid schedule");

            WorkingHoursSchedule workingHours = JsonSerializer.Deserialize<WorkingHoursSchedule>(schedule);

            IEnumerable<WorkingHoursSlot> slots = FluentSwitch.On(DateTime.Now.DayOfWeek)
                .Case(DayOfWeek.Monday).Then(workingHours.Monday)
                .Case(DayOfWeek.Tuesday).Then(workingHours.Tuesday)
                .Case(DayOfWeek.Wednesday).Then(workingHours.Wednesday)
                .Case(DayOfWeek.Thursday).Then(workingHours.Thursday)
                .Case(DayOfWeek.Friday).Then(workingHours.Friday)
                .Case(DayOfWeek.Saturday).Then(workingHours.Saturday)
                .Case(DayOfWeek.Sunday).Then(workingHours.Sunday).Default(null);

            if (slots == null)
                return false;

            foreach (var slot in slots) {
                if (DateTime.Now.Date.Add(slot.FromOfDay) <= DateTime.Now && DateTime.Now < DateTime.Now.Date.Add(slot.ToOfDay))
                    return true;
            }

            return false;
        }

        public static bool IsValidJson(this string json) {
            try {
                System.Text.Json.JsonDocument.Parse(json);
                return true;
            }
            catch { }

            return false;
        }

        private static bool CheckMatch(string source, string regularExpression) {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(regularExpression))
                return false;

            Regex regex = new Regex(regularExpression, RegexOptions.IgnoreCase);
            return regex.Match(source).Success;
        }
    }
}