using System.Collections.Generic;
using CoinMarketCap.Shared.Model;
using Newtonsoft.Json;

namespace CoinMarketCap.Public.Model
{
    public class ListingsData : ResponseDataModel
    {
        [JsonProperty("data")]
        public List<Listings> Data { get; set; }
        [JsonProperty("metadata")]
        public ListingsMetadata Metadata { get; set; }
    }
}