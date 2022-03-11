// LitresAuthor model

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace PocketApi.Models
{
    public class LitresAuthor
    {
        [JsonProperty("author_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        public override string ToString()
        {
            return $"Author #{Id} - ({Name})";
        }
    }
}