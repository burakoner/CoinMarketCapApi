﻿using Newtonsoft.Json;
using System;

namespace CoinMarketCap.Pro.Model.Cryptocurrency
{
    public class Quote
    {
        [JsonProperty("price")]
        public decimal? Price { get; set; }
        [JsonProperty("volume_24h")]
        public decimal? Volume24H { get; set; }
        [JsonProperty("percent_change_1h")]
        public decimal? PercentChange1H { get; set; }
        [JsonProperty("percent_change_24h")]
        public decimal? PercentChange24H { get; set; }
        [JsonProperty("percent_change_7d")]
        public decimal? PercentChange7D { get; set; }
        [JsonProperty("market_cap")]
        public decimal? MarketCap { get; set; }
        [JsonProperty("last_updated")]
        public DateTime? LastUpdated { get; set; }
    }
}