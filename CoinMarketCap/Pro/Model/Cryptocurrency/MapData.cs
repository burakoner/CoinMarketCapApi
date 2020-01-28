using CoinMarketCap.Shared.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCap.Pro.Model.Cryptocurrency
{
    public class MapData : ResponseDataModel
    {
        [JsonProperty("data")]
        public List<Map> Data { get; set; }
        [JsonProperty("status")]
        public Status Status { get; set; }
    }
}