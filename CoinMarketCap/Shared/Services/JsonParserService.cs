using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoinMarketCap.Shared.Services
{
    public class JsonParserService
    {
        public static async Task<T> ParseResponse<T>(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                httpResponseMessage.EnsureSuccessStatusCode();
                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                var listingsResponse = JsonConvert.DeserializeObject<T>(responseContent, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                return listingsResponse;
            }

            return default(T);
        }
    }
}