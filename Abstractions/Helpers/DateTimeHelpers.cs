using System;

namespace Filuet.Infrastructure.Abstractions.Helpers
{
    public static class DateTimeHelpers
    {
        public static long ToUnixTimestamp(this DateTime dt)
        {
            long unixTimestamp = (long)(dt.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return unixTimestamp;
        }

        public static DateTime FromUnixTimestamp(this long ut)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            start = start.AddSeconds(ut);
            return start;
        }
    }
}