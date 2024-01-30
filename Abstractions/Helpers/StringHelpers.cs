using Filuet.Infrastructure.Abstractions.Business.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Filuet.Infrastructure.Abstractions.Helpers
{
    public static class StringHelpers
    {
        public static string GetPaymentSystem(this string cardNumber)
        {
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

        public static bool IsGuid(this string source)
        {
            if (source == null)
                return false;

            source = source.Trim().Replace(" ", "");

            if (string.IsNullOrWhiteSpace(source) || source.Length < 32)
                return false;

            return CheckMatch(source, @"^[{(]?[0-9A-F]{8}[-]?([0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$");
        }

        public static bool IsEmail(this string input)
            => CheckMatch(input, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public static bool IsPhone(this string mobile)
            => CheckMatch(mobile, @"^(\+\d{1,2}\s)\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$");

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

        public static bool IsValidJson(this string json)
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