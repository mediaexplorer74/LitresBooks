// LitresItemsBody

using PocketApi.Models;
using Newtonsoft.Json.Serialization;//using System.Text.Json.Serialization;
using Newtonsoft.Json; 

namespace PocketApi.RestApiRequestModels
{
   
    internal class LitresItemsBody
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("sid")]
        public string Sid { get; set; }

        /*
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        */

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("detailType")]
        public string DetailType { get; set; }

        [JsonProperty("since")]
        public double Since { get; set; }

        [JsonIgnore]
        public PocketItemState State { get; set; }

        [JsonProperty("state")]
        public string StateString
        {
            get
            {
                return this.State.ToString().ToLower();
            }
        }

        [JsonIgnore]
        public PocketItemSort Sort { get; set; }

        [JsonProperty("sort")]
        public string SortString
        {
            get
            {
                return this.Sort.ToString().ToLower();
            }
        }
    }//class end

}
