// RequestToken model

using Newtonsoft.Json.Serialization;//System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PocketApi.Models
{
   
    public class RequestToken
    {
        [JsonProperty("sid")]
        public string Sid { get; set; }
        
        [JsonProperty("user-id")]
        public object UserId { get; set; }

    }//class end

}//namespace end
