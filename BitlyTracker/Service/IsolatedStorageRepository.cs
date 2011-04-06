using System;
using System.IO.IsolatedStorage;
using BitlyTracker.Models;
using IsolatedStorageExtensions;


namespace BitlyTracker.Service
{
    public class IsolatedStorageRepository : IRepository
    {
        public BitlyLogin GetCurrentUser()
        {
           var returnValue = IsolatedStorageHelper.GetApplicationSetting("user");

           if (returnValue == null)
               return new MissingBitlyLogin();
           return returnValue as BitlyLogin; 
        }

        public void SaveUser(BitlyLogin login)
        {
            IsolatedStorageHelper.SaveApplicationSetting("user", login);
        }

        public void DeleteCurrentUser()
        {
            throw new NotImplementedException();
        }
    }
}
