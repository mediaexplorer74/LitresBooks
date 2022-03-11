using PocketApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketApi
{
    /*
    public partial class PocketClient
    {
        public Uri ObtainAuthorizeRequestTokenRedirectUri(RequestToken RequestToken, Uri RedirectUri)
        {
            Uri uri = 
                new Uri
                (
                    $"https://getpocket.com/auth/authorize?request_token={RequestToken.Code}" +
                    $"&redirect_uri={RedirectUri.ToString()}"
                );
            return uri;
        }
    }
    */


    public partial class LitresClient
    {
        // TODO
        public Uri ObtainAuthorizeRequestTokenRedirectUri(RequestToken RequestToken, Uri RedirectUri)
        {
            Uri uri =
                new Uri
                (
                    $"https://robot.litres.ru/pages/catalit_browser/?sid=={RequestToken.Sid}" +
                    $"&my=1&limit=5"
                );
            return uri;
        }
    }
}
