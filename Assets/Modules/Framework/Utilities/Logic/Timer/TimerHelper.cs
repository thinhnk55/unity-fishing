using System;

namespace Framework
{
    public class TimerHelper
    {
        public static long NowFrom0001From1970(long timeFrom1970InMili)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime dotNetEpoch = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long tickCountBetweenEpochs = unixEpoch.Ticks - dotNetEpoch.Ticks;
            return timeFrom1970InMili * 10000 + tickCountBetweenEpochs;
        }
        public static int TickToSecond(long ticks)
        {
            return (int)(ticks / 10000000);
        }
        public static string Hour_Minute_Second_1(long time)
        {
            return $"{TimeSpan.FromSeconds(time).Hours:D2}:{TimeSpan.FromSeconds(time).Minutes:D2}:{TimeSpan.FromSeconds(time).Seconds:D2}";
        }
        public static string Hour_Minute_Second_2(long time)
        {
            return $"Hour:{TimeSpan.FromSeconds(time).Hours:D2} Minute:{TimeSpan.FromSeconds(time).Minutes:D2} Second:{TimeSpan.FromSeconds(time).Seconds:D2}";
        }
    }
}
