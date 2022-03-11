using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Serialization;//using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PocketApi.Models
{
    public class AccessData
    {
        [JsonProperty("sid")]
        public string Sid { get; set; }

        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }
        
        [JsonProperty("pwd")]
        public string Password { get; set; }
    }
}
