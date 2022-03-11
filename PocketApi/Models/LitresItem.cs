// LitresItem model

using PocketApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Serialization; //System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PocketApi.Models
{
 
    public class LitresItem
    {
        [JsonProperty("item_id")]
        public string Id { get; set; }

        [JsonProperty("resolved_id")]
        public string ResolvedId { get; set; }

        [JsonProperty("given_url")]
        public string GivenUrl { get; set; }

        [JsonProperty("given_title")]
        public string GivenTitle { get; set; }

        [JsonProperty("favorite")]
        public string Favorite { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time_added")]
        public string TimeAdded { get; set; }

        [JsonProperty("time_updated")]
        public string TimeUpdated { get; set; }

        [JsonProperty("time_read")]
        public string TimeRead { get; set; }

        [JsonProperty("time_favorited")]
        public string TimeFavorited { get; set; }

        [JsonProperty("sort_id")]
        public int SortId { get; set; }

        [JsonProperty("resolved_title")]
        public string ResolvedTitle { get; set; }

        [JsonProperty("resolved_url")]
        public string ResolvedUrl { get; set; }

        [JsonProperty("excerpt")]
        public string Excerpt { get; set; }

        [JsonProperty("is_article")]
        public string IsArticle { get; set; }

        [JsonProperty("is_index")]
        public string IsIndex { get; set; }

        [JsonProperty("has_video")]
        public string HasVideo { get; set; }

        [JsonProperty("has_image")]
        public string HasImage { get; set; }

        [JsonProperty("word_count")]
        public string WordCount { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("time_to_read")]
        public int TimeToRead { get; set; }

        [JsonProperty("top_image_url")]
        public string TopImageUrl { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("author_array")]
        public List<LitresAuthor> Authors { get; set; }

        [JsonProperty("image")]
        public LitresImage Image { get; set; }

        [JsonProperty("image_array")]
        public List<LitresImage> Images { get; set; }

        [JsonProperty("listen_duration_estimate")]
        public int ListenDurationEstimate { get; set; }

        
        override public string ToString()
        {
            return $"Item #{Id} - ({ResolvedTitle})";
        }
        
    }//class end
}
