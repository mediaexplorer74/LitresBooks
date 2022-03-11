using PocketApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketApi
{
    /*
    public partial class PocketClient
    {
        private AccessToken _accessToken;
      

        private string _consumerKey;
        
        
        public PocketClient(AccessToken accessToken)
            : this(accessToken.ConsumerKey)
        {
            _accessToken = accessToken;
        }

        public PocketClient(string consumerKey)
        {
            _consumerKey = consumerKey;
            this.InitializeHttpClient();
        }        

    }
    */


    public partial class LitresClient
    {
        public AccessData _accessData;

        private string _userKey;

        public LitresClient(AccessData accessData)
            : this(accessData.Login)
        {
            _accessData = accessData;
        }

        public LitresClient(string userKey)
        {
            _userKey = userKey;
            this.InitializeHttpClient();
        }
        

    }
}
