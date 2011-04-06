using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BitlyTracker.Models;

namespace BitlyTracker.Service
{
    /// <summary>
    /// Interface for writing and reading data to / from IsolatedStorage
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Since BitlyTracker handles only one account a time currently, get the current user from storage.
        /// </summary>
        /// <returns>valid Bitly OAuth credentials</returns>
        BitlyLogin GetCurrentUser();

        /// <summary>
        /// Saves a new Bit.ly user to IsolatedStorage
        /// </summary>
        /// <param name="login"></param>
        void SaveUser(BitlyLogin login);

        /// <summary>
        /// Since BitlyTracker handles only one account a time currently, delete the current user from storage.
        /// </summary>
        void DeleteCurrentUser();
    }
}
