using CoinMarketCap.Shared.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCap.Pro.Model.Cryptocurrency
{
    public class Quotes
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("circulating_supply")]
        public long CirculatingSupply { get; set; }
        [JsonProperty("total_supply")]
        public long TotalSupply { get; set; }
        [JsonProperty("max_supply")]
        public long MaxSupply { get; set; }
        [JsonProperty("date_added")]
        public DateTime? DateAdded { get; set; }
        [JsonProperty("last_updated")]
        public DateTime? LastUpdated { get; set; }
        [JsonProperty("num_market_pairs")]
        public int NumberOfMarketPairs { get; set; }
        [JsonProperty("cmc_rank")]
        public int CoinMarketCapRank { get; set; }
        [JsonProperty("quote")]
        public Dictionary<string, Quote> QuoteList { get; set; }
    }
}