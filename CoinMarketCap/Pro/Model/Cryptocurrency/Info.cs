﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCap.Pro.Model.Cryptocurrency
{
    public class Info
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("logo")]
        public string Logo { get; set; }
        [JsonProperty("tags")]
        public string[] Tags { get; set; }
        [JsonProperty("urls")]
        public Dictionary<string,string[]> Urls { get; set; }
    }
}