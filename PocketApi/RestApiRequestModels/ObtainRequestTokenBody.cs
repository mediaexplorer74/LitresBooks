using Newtonsoft.Json.Serialization;//using System.Text.Json.Serialization;
using Newtonsoft.Json; //using System.Text.Json;

namespace PocketApi.RestApiRequestModels
{
    /*
    class ObtainRequestTokenBody
    {
        [JsonProperty("consumer_key")]
        public string ConsumerKey { get; set; }
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }
    }
    */

    class ObtainRequestDataBody
    {
        /*
        [JsonProperty("userkey")]
        public string UserKey { get; set; }

        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }
        */
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("pwd")]
        public string Password { get; set; }
    }
}
