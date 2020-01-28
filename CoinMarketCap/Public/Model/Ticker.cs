using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoinMarketCap.Public.Model
{
    public class Ticker
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("website_slug")]
        public string WebsiteSlug { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("circulating_supply")]
        public long? CirculatingSupply { get; set; }

        [JsonProperty("total_supply")]
        public long? TotalSupply { get; set; }

        [JsonProperty("max_supply")]
        public long? MaxSupply { get; set; }

        [JsonProperty("quotes")]
        public Dictionary<string, TickerQuotes> Quotes { get; set; }

        [JsonProperty("last_updated")]
        public long? LastUpdated { get; set; }
    }
}