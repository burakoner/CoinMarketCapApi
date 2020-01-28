using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoinMarketCap.Public.Model
{
    public class Global
    {
        [JsonProperty("active_cryptocurrencies")]
        public int ActiveCryptocurrencies { get; set; }
        [JsonProperty("active_markets")]
        public int ActiveMarkets { get; set; }
        [JsonProperty("bitcoin_percentage_of_market_cap")]
        public double BitcoinPercentageOfMarketCap { get; set; }
        [JsonProperty("quotes")]
        public Dictionary<string,GlobalQuotes> Quotes { get; set; }
        [JsonProperty("last_updated")]
        public int LastUpdated { get; set; }
    }
}