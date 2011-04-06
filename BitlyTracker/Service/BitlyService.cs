using System;
using System.Collections.Generic;
using BitlyTracker.Helpers;
using BitlyTracker.Models;
using Hammock;
using Hammock.Web;
using System.Json;
using Newtonsoft.Json;

namespace BitlyTracker.Service
{
    public class BitlyService : IBitlyService
    {
        private RestClient _client;
        private RestClient _sslClient;

        public BitlyService(BitlySettings settings)
        {
            Settings = settings;
            _client = new RestClient {Authority = "http://api.bitly.com/", VersionPath = "v3"};
            _sslClient = new RestClient {Authority = "https://api-ssl.bitly.com/", VersionPath = "v3"};
        }

        #region Implementation of IBitlyService

        public BitlySettings Settings { get; private set; }

        public string GetAuthorizeUrl()
        {
            return string.Format("https://bit.ly/oauth/authorize?client_id={0}&redirect_uri={1}", Settings.ConsumerKey, Settings.RedirectUrl);
        }

        public void BeginGetAccessToken(string code, RestCallback callback)
        {
            var oauthClient = new RestClient {Authority = _sslClient.Authority};

            //Build an OAuth request manually - the Bit.ly documentation didn't indicate that I needed signatures
            var request = new RestRequest {Method = WebMethod.Post, Path = "/oauth/access_token"};
            
            request.AddParameter("client_id", Settings.ConsumerKey);
            request.AddParameter("client_secret", Settings.ConsumerSecret);
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", Settings.RedirectUrl);

            oauthClient.BeginRequest(request, callback);

        }

        public BitlyLogin EndGetAccessToken(RestResponse response)
        {
            try
            {
                var parameters = ParameterHelper.ExtractQueryParameters(response.Content);

                return new BitlyLogin
                           {
                               access_token = parameters["access_token"],
                               apiKey = parameters["apiKey"],
                               login = parameters["login"]
                           };
            }
            catch(Exception ex)
            {
                return new MissingBitlyLogin();
            }
        }


        public bool Validate(BitlyLogin user)
        {
            throw new NotImplementedException();
        }      

        public void BeginUserRealtimeLinks(BitlyLogin user, RestCallback callback)
        {
            var request = new RestRequest {Path = "user/realtime_links"};
            request.AddParameter("format", "json");
            request.AddParameter("access_token", user.access_token);

            _sslClient.BeginRequest(request, callback);
        }

        public BitlyRealtimeLinksResponse EndUserRealtimeLinks(RestResponse response)
        {
            throw new NotImplementedException();
        }

        #endregion


        public void BeginUserClicks(BitlyLogin user, int days, RestCallback callback)
        {
            var request = new RestRequest { Path = "user/clicks" };
            request.AddParameter("format", "json");
            request.AddParameter("access_token", user.access_token);

            _sslClient.BeginRequest(request, callback);
        }

        public BitlyUserClicksResponse EndUserClicks(RestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var bitlyResponse = JsonConvert.DeserializeObject<BitlyUserClicksResponse>(response.Content);
                return bitlyResponse;
            }

            return new MissingBitlyUserClicksResponse();
        }
    }
}
