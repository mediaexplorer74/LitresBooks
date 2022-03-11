using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;//using System.Text.Json.Serialization;
using Newtonsoft.Json; //using System.Text.Json;

namespace PocketApi.RestApiRequestModels
{
    internal class ObtainAccessTokenBody
    {
        //[JsonProperty("consumer_key")]
        //public string ConsumerKey { get; set; }
        //[JsonProperty("code")]
        //public string RequestTokenCode { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("pwd")]
        public string Pwd { get; set; }
    }

    /*
    internal class ObtainAccessDataBody
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("sid")]
        public string RequestSidCode { get; set; }
    }
    */
}
