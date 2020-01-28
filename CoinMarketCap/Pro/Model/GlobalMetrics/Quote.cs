using Newtonsoft.Json;
using System;

namespace CoinMarketCap.Pro.Model.GlobalMetrics
{
    public class Quote
    {
        [JsonProperty("total_market_cap")]
        public decimal TotalMarketCap { get; set; }
        [JsonProperty("total_volume_24h")]
        public decimal TotalVolume24H { get; set; }
        [JsonProperty("total_volume_24h_reported")]
        public decimal TotalVolume24HReported { get; set; }


        [JsonProperty("altcoin_market_cap")]
        public decimal AltcoinMarketCap { get; set; }
        [JsonProperty("altcoin_volume_24h")]
        public decimal AltcoinVolume24H { get; set; }
        [JsonProperty("altcoin_volume_24h_reported")]
        public decimal AltcoinVolume24HReported { get; set; }


        [JsonProperty("last_updated")]
        public DateTime? LastUpdated { get; set; }
    }
}