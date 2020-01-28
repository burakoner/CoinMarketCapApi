using CoinMarketCap.Shared.Model;
using Newtonsoft.Json;

namespace CoinMarketCap.Public.Model
{
    public class GlobalData: ResponseDataModel
    {
        [JsonProperty("data")]
        public Global Data { get; set; }
        [JsonProperty("metadata")]
        public GlobalMetadata Metadata { get; set; }
    }
}