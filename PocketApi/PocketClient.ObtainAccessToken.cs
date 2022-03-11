using PocketApi.Models;
using PocketApi.RestApiRequestModels;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json; //using System.Text.Json;
using System.Threading.Tasks;

namespace PocketApi
{
    /*
    public partial class PocketClient
    {
        private static Uri _obtainAccessTokenUri = new Uri($"https://getpocket.com/v3/oauth/authorize");

        
        public async Task<AccessToken> ObtainAccessTokenAsync(RequestToken RequestToken)
        {
            string response = await ApiPostAsync(
                  _obtainAccessTokenUri,
                   new ObtainAccessTokenBody()
                   {
                       ConsumerKey = _consumerKey,
                       RequestTokenCode = RequestToken.Code
                   }
               );
            AccessToken token = JsonConvert.DeserializeObject<AccessToken>(response);
            token.ConsumerKey = _consumerKey;

            this._accessToken = token;
            return token;
        }
       
    }
    */


    public partial class LitresClient
    {
        private static Uri _obtainAccessDataUri = new Uri($"https://robot.litres.ru/pages/catalit_authorise");

        public async Task<AccessData> ObtainAccessDataAsync(RequestToken RequestToken)
        {
            string response = await ApiPostAsync(
              _obtainAccessDataUri,
               new ObtainAccessTokenBody()
               {
                   Login = _userKey,//ConsumerKey = _consumerKey,
                   Pwd = RequestToken.Sid//RequestSidCode = RequestToken.Sid//RequestTokenCode = RequestToken.Code
               }
           );

            // atoken - "access token"
            AccessData atoken = JsonConvert.DeserializeObject<AccessData>(response);
            atoken.Login = _userKey;

            this._accessData = atoken;//_accessToken = atoken;
            return atoken;
        }//

    }//class end
}
