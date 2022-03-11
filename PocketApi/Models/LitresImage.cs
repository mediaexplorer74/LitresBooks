// LitresImage model

using System;
using Newtonsoft.Json.Serialization;//using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PocketApi.Models
{
    public class LitresImage
    {
        [JsonProperty("image_id")]
        public string Id { get; set; }

        [JsonProperty("src")]
        public string Source { get; set; }

        [JsonProperty("width")]
        public string WidthString { get; set; }
        public double Width {
            get
            {
                if (double.TryParse(WidthString, out double width))
                    return width;
                else
                    return 0;
            }
        }

        [JsonProperty("height")]
        public string HeightString { get; set; }
        public double Height
        {
            get
            {
                if (double.TryParse(HeightString, out double height))
                    return height;
                else
                    return 0;
            }
        }

        [JsonProperty("credit")]
        public string Credit { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("item_id")]
        public string ItemId { get; set; }
    }
}