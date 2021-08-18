using System;

namespace BookmarksManager
{
    public static class DateTimeHelper
    {
        public static long ToUnixTimestamp(this DateTime time)
        {
            try
            {
                return (long)((DateTimeOffset)time).ToUnixTimeSeconds();
            }
            catch (ArgumentOutOfRangeException)
            {
                DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return (long)(time > UnixEpoch ? DateTimeOffset.MaxValue.ToUnixTimeSeconds() : DateTimeOffset.MinValue.ToUnixTimeSeconds());
            }
        }

        public static DateTime? FromUnixTimeStamp(long? unixTimeStamp)
        {
            if (!unixTimeStamp.HasValue || unixTimeStamp < 0)
                return null;
            unixTimeStamp = takeNDigits(unixTimeStamp.Value, 10);
            return DateTimeOffset.FromUnixTimeSeconds((long)unixTimeStamp).UtcDateTime;
        }

        public static DateTime? FromUnixTimeStamp(string unixTimeStamp)
        {
            if (!string.IsNullOrEmpty(unixTimeStamp) && long.TryParse(unixTimeStamp, out long unixTime))
            {
                return FromUnixTimeStamp(unixTime);
            }
            return null;
        }
        private static long takeNDigits(long number, int n)
        {
            number = Math.Abs(number);
            if (number == 0)
                return number;
            int numberOfDigits = (int)Math.Floor(Math.Log10(number) + 1);
            if (numberOfDigits >= n)
                return (long)Math.Truncate((number / Math.Pow(10, numberOfDigits - n)));
            else
                return number;
        }
    }
}