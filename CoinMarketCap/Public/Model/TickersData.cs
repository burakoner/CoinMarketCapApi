using System.Collections.Generic;
using CoinMarketCap.Shared.Model;
using Newtonsoft.Json;

namespace CoinMarketCap.Public.Model
{
    public class TickersData:ResponseDataModel
    {
        [JsonProperty("data")]
        public Dictionary<string, Ticker> Data { get; set; }

        [JsonProperty("metadata")]
        public TickerMetadata Metadata { get; set; }
    }
}