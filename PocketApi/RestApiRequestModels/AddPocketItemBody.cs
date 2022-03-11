using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;//using System.Text.Json.Serialization;
using Newtonsoft.Json; //using System.Text.Json;


namespace PocketApi.RestApiRequestModels
{
    internal class AddPocketItemBody
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonIgnore]
        public List<string> Tags { get; set; }
        
        [JsonProperty("tags")]
        public string TagsString
        {
            get
            {
                if (Tags == null)
                    return "";

                string tagString = "";
                foreach(string tag in Tags)
                {
                    tagString = string.Concat(tagString, tag, ",");
                }
                return tagString;
            }
        }

        [JsonIgnore]
        public Uri Uri { get; set; }

        [JsonProperty("url")]
        public string UriString
        {
            get
            {
                return Uri.ToString();
            }
        }

        [JsonProperty("tweet_id")]
        public string TweetId { get; set; }
        
        [JsonProperty("consumer_key")]
        public string ConsumerKey { get; set; }
        
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
