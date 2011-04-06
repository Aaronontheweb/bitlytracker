using System;
using System.Net;
using System.Collections.Generic;
using BitlyTracker.ViewModels;
using BitlyTracker.Models;
using BitlyTracker.Helpers;

namespace BitlyTracker.Converters
{
    public class UserClicksConverter
    {
        public static IList<UserClick> ExtractClicks(BitlyUserClicksResponse response)
        {
            var finalList = new List<UserClick>();

            foreach (var rawClick in response.data.clicks)
            {
                var dtoClick = new UserClick { TotalClicks = rawClick.clicks, Date = DateTimeHelper.ConvertFromUnixTimestamp((long)rawClick.day_start) };
                finalList.Add(dtoClick);
            }

            return finalList;
        }
    }
}
