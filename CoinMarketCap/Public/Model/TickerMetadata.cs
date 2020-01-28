using Newtonsoft.Json;

namespace CoinMarketCap.Public.Model
{
    public class TickerMetadata
    {
        [JsonProperty("timestamp")]
        public long? Timestamp { get; set; }

        [JsonProperty("num_cryptocurrencies")]
        public long? NumCryptocurrencies { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }
    }
}