using CoinMarketCap.Shared.Model;
using Newtonsoft.Json;

namespace CoinMarketCap.Public.Model
{
    public class TickerByIdData:ResponseDataModel
    {
        [JsonProperty("data")]
        public Ticker Data { get; set; }

        [JsonProperty("metadata")]
        public TickerMetadata Metadata { get; set; }
    }
}