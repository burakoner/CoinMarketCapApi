using CoinMarketCap.Shared.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCap.Pro.Model.GlobalMetrics
{
    public class QuotesData : ResponseDataModel
    {
        [JsonProperty("data")]
        public  Quotes Data { get; set; }
        [JsonProperty("status")]
        public Status Status { get; set; }
    }
}