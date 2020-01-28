using CoinMarketCap.Shared.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CoinMarketCap.Shared.Extensions;

namespace CoinMarketCap.Pro.Model.GlobalMetrics
{
    public class Quotes
    {
        [JsonProperty("active_cryptocurrencies")]
        public int ActiveCryptocurrencies { get; set; }
        [JsonProperty("active_market_pairs")]
        public int ActiveMarketPairs { get; set; }
        [JsonProperty("active_exchanges")]
        public int ActiveExchanges { get; set; }
        [JsonProperty("btc_dominance")]
        public double BTCDominance { get; set; }
        [JsonProperty("eth_dominance")]
        public double ETHDominance { get; set; }
        [JsonProperty("quote")]
        public Dictionary<string, Quote> QuoteList { get; set; }
        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}