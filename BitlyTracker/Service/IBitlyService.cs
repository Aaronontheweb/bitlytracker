using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BitlyTracker.Models;
using Hammock;

namespace BitlyTracker.Service
{
    public interface IBitlyService
    {
        BitlySettings Settings { get; }

        string GetAuthorizeUrl();
        void BeginGetAccessToken(string code, RestCallback callback);
        BitlyLogin EndGetAccessToken(RestResponse response);
        bool Validate(BitlyLogin user);        

        //Methods for querying a user's real-time links
        void BeginUserRealtimeLinks(BitlyLogin user, RestCallback callback);
        BitlyRealtimeLinksResponse EndUserRealtimeLinks(RestResponse response);

        //Methods for querying a user's clicks for the past N days
        void BeginUserClicks(BitlyLogin user, int days, RestCallback callback);
        BitlyUserClicksResponse EndUserClicks(RestResponse response);
    }
}
