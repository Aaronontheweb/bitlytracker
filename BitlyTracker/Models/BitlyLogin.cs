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

namespace BitlyTracker.Models
{
    /// <summary>
    /// Represents a user who has successfully authenticated
    /// </summary>
    public class BitlyLogin
    {
        /// <summary>
        /// the OAuth access token for specified user
        /// </summary>
        public string access_token { get; set; }
        
        /// <summary>
        ///  the end-user’s bit.ly username
        /// </summary>
        public string login { get; set; }

        /// <summary>
        /// the end-user’s bit.ly API key
        /// </summary>
        public string apiKey { get; set; }
    }

    /// <summary>
    /// Class used for the special case pattern to indicate that we don't have an active user yet
    /// </summary>
    public class MissingBitlyLogin : BitlyLogin{}
}
