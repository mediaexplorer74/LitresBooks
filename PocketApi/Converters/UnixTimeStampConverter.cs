using System;
using System.Collections.Generic;
using System.Text;

namespace PocketApi.Converters
{
    public class UnixTimestampConverter
    {
        private static DateTime unixEpochDateTime = new DateTime(1970, 1, 1, 0, 0, 0);

        public static double ToUnixtimestamp(DateTime dateTimeToConvert)
        {
            TimeSpan unixTimespan = dateTimeToConvert.Subtract(unixEpochDateTime);
            return Math.Round(unixTimespan.TotalSeconds);
        }

        public static DateTime ToDateTime(double unixTimestamp)
        {
            return unixEpochDateTime.AddSeconds(unixTimestamp);
        }
    }
}
