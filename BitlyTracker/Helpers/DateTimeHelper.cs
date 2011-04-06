using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BitlyTracker.Helpers
{
    using System;

    public static class DateTimeHelper
    {
        public static DateTime UnixOrigin
        {
            get { return new DateTime(1970, 1, 1, 0, 0, 0, 0); }
        }

        public static DateTime ConvertFromUnixTimestamp(long timestamp)
        {
            return UnixOrigin.AddSeconds((long)timestamp);
        }

        public static long ConvertToUnixTimestamp(DateTime dateTime)
        {
            TimeSpan differnce = dateTime - UnixOrigin;

            return (long)differnce.TotalSeconds;
        }
    }
}
