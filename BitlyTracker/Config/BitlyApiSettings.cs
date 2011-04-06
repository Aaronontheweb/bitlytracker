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
using BitlyTracker.Models;

namespace BitlyTracker.Config
{
    public class BitlyApiSettings
    {
        public const string BitlyConsumerKey = "YOUR KEY";
        public const string BitlyConsumerSecret = "YOUR SECRET";
        public const string RedirectUrl = "http://www.stannardlabs.com/bitlytracker/auth";

        public static BitlySettings DefaultSettings
        {
            get
            {
                return new BitlySettings
                           {
                               ConsumerKey = BitlyConsumerKey,
                               ConsumerSecret = BitlyConsumerSecret,
                               RedirectUrl = RedirectUrl
                           };
            }
        }
    }
}
