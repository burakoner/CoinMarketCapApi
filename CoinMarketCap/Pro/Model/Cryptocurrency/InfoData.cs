using System;
using CoinMarketCap.Shared.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCap.Pro.Model.Cryptocurrency
{
    public class InfoData : ResponseDataModel
    {
        [JsonProperty("data")]
        public Dictionary<string, Info> Data { get; set; }
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("date_added")]
        public DateTimeOffset? DateAdded { get; set; }
        [JsonProperty("twitter_username")]
        public string TwitterUsername { get; set; }
        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }
    }
}