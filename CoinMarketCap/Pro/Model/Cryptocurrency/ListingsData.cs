using CoinMarketCap.Shared.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCap.Pro.Model.Cryptocurrency
{
    public class ListingsData : ResponseDataModel
    {
        [JsonProperty("data")]
        public Listings[] Data { get; set; }
        [JsonProperty("status")]
        public Status Status { get; set; }
    }
}