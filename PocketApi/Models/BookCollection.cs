using System;
using System.Collections.Generic;
using System.Text;
// BookCollection

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace PocketApi.Models
{
    
    public class BookCollection
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("consumerkey")]
        public string ConsumerKey { get; set; }
    }
    
}
