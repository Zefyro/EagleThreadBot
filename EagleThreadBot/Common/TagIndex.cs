using System;

using Newtonsoft.Json;

namespace EagleThreadBot.Common
{
    public class TagIndex
    {
        // Get the tag index
        [JsonProperty("index")]
        public Index[] index { get; set; }
    }
    public class Index
    {
        // Get the tag identifier from index
        [JsonProperty("identifier")]
        public String identifier { get; set; }

        // Get the tag aliases from index
        [JsonProperty("aliases")]
        public String[] aliases { get; set; }

        // Get the tag url from index
        [JsonProperty("url")]
        public String url { get; set; }

        // Get if the tag is embed
        [JsonProperty("isEmbed")]
        public Boolean isEmbed { get; set; }

        // Get if the tag is paged
        [JsonProperty("isPaged")]
        public Boolean isPaged { get; set; }
    }
}