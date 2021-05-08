using System;
using CoinMarketCap.Shared.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinMarketCap.Pro.Model.Cryptocurrency
{
    public class InfoData : ResponseDataModel
    {
        [JsonProperty("data")]
        public Dictionary<string, Info> Data { get; set; }
        [JsonProperty("status")]
        public Status Status { get; set; }


    }
}