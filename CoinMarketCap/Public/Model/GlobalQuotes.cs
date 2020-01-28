using Newtonsoft.Json;

namespace CoinMarketCap.Public.Model
{
    public class GlobalQuotes
    {
        [JsonProperty("total_market_cap")]
        public decimal TotalMarketCap { get; set; }
        [JsonProperty("total_volume_24h")]
        public decimal TotalVolume24H { get; set; }
    }
}