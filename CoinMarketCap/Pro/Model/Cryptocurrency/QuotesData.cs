using CoinMarketCap.Shared.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCap.Pro.Model.Cryptocurrency
{
    public class QuotesData : ResponseDataModel
    {
        [JsonProperty("data")]
        public Dictionary<string, Quotes> Data { get; set; }
        [JsonProperty("status")]
        public Status Status { get; set; }
    }
}