using System;
using System.Collections.Generic;


namespace BitlyTracker.Helpers
{
    public class ParameterHelper
    {
        /// <summary>
        /// Takes a valid Uri and returns a list of all of its query parameter arguments
        /// </summary>
        /// <param name="incUri">A valid Uri object</param>
        /// <returns>A ghetto NameValueCollection implemented using a Dictionary :(</returns>
        public static Dictionary<string, string> ExtractQueryParameters(string incUri)
        {
            var nameValueCollection = new Dictionary<string, string>();
            var parameters = incUri.Split('&');

            foreach (var parameter in parameters)
            {
                if (!parameter.Contains("=")) continue;
                var nameValuePair = parameter.Split('=');
                if (nameValuePair[0].Contains("?"))
                    nameValuePair[0] = nameValuePair[0].Replace("?", "");
                nameValueCollection.Add(nameValuePair[0], Uri.UnescapeDataString(nameValuePair[1]));
            }
            return nameValueCollection;
        }
    }
}
