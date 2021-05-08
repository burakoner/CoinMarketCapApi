using Newtonsoft.Json;
using System;

namespace CoinMarketCap.Pro.Model
{
    public class Status
    {
        [JsonProperty("timestamp")]
        public DateTime TimeStamp { get; set; }
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
        [JsonProperty("elapsed")]
        public int Elapsed { get; set; }
        [JsonProperty("credit_count")]
        public int CreditCount { get; set; }
        
        [JsonProperty("notice")]
        public string Notice { get; set; }
    }
}