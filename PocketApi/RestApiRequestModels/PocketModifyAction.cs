using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;//using System.Text.Json.Serialization;
using Newtonsoft.Json; //using System.Text.Json;

namespace PocketApi.RestApiRequestModels
{
    internal class LitresModifyAction
    {
        [JsonIgnore]
        public LitresModifyActionType Action { get; set; }
       
        [JsonProperty("action")]
        public string ActionString
        {
            get
            {
                return Action.ToString().ToLower();
            }
        }

        [JsonProperty("item_id")]
        public string ItemId { get; set; }
        
        [JsonProperty("time")]
        public string UnixTimestamp { get; set; }
    }
}
