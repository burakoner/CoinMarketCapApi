using CoinMarketCap.Pro;
using CoinMarketCap.Pro.Model;
using CoinMarketCap.Pro.Parameters;
using CoinMarketCap.Shared.Services;
using CoinMarketCap.Shared.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinMarketCap
{
    /// <summary>
    /// Coin Market Cap Pro Api Client
    /// </summary>
    public class ProClient : IDisposable
    {
        public string ProApiKey { get; set; }
        public int CreditsUsedToday
        {
            get
            {
                return this.statusList.Where(x => x.TimeStamp >= DateTime.UtcNow.Date).Sum(x => x.CreditCount);
            }
        }
        public int CreditsUsedYesterday
        {
            get
            {
                return this.statusList.Where(x => x.TimeStamp >= DateTime.UtcNow.AddDays(-1).Date && x.TimeStamp < DateTime.UtcNow.Date).Sum(x => x.CreditCount);
            }
        }
        public int CreditsUsedThisMonth
        {
            get
            {
                return this.statusList.Where(x => x.TimeStamp >= new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1, 0, 0, 0)).Sum(x => x.CreditCount);
            }
        }
        private List<Status> statusList;

        public ProClient(string apiKey)
        {
            this.ProApiKey = apiKey;
            this.statusList = new List<Status>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _isDisposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            if (disposing)
            {
                // _httpClient?.Dispose();
            }
            _isDisposed = true;
        }

        /// <summary>
        /// Returns a paginated list of all cryptocurrencies by CoinMarketCap ID.
        /// <para>We recommend using this convenience endpoint to lookup and utilize our unique cryptocurrency id across all endpoints as typical identifiers like ticker symbols can match multiple cryptocurrencies and change over time.</para>
        /// <para>As a convenience you may pass a comma-separated list of cryptocurrency symbols as symbol to filter this list to only those you require.</para>
        /// </summary>
        /// <param name="listing_status">
        /// string, Default "active"
        /// <para>Valid Values "active" "inactive"</para>
        /// <para>Only active coins are returned by default. Pass 'inactive' to get a list of coins that are no longer active.</para>
        /// </param>
        /// <param name="start">
        /// integer >=1, Default 1
        /// <para>Optionally offset the start (1-based index) of the paginated list of items to return.</para>
        /// </param>
        /// <param name="limit">
        /// integer [1 ... 5000]
        /// <para>Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size.</para>
        /// </param>
        /// <param name="symbol">
        /// string
        /// <para>Optionally pass a comma-separated list of cryptocurrency symbols to return CoinMarketCap IDs for. If this option is passed, other options will be ignored.</para>
        /// </param>
        /// <returns></returns>
        public Pro.Model.Cryptocurrency.MapData CryptocurrencyMap(ListingStatus listing_status = ListingStatus.active, int? start = 1, int? limit = 5000, string symbol = null)
        {
            return this.CryptocurrencyMapAsync(listing_status, start, limit, symbol).Result;
        }

        public async Task<Pro.Model.Cryptocurrency.MapData> CryptocurrencyMapAsync(ListingStatus listing_status = ListingStatus.active, int? start = 1, int? limit = 5000, string symbol = null)
        {
            HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(Pro.Config.Cryptocurrency.CoinMarketCapProApiUrl) };
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", this.ProApiKey);

            var url = QueryStringService.AppendQueryString(Pro.Config.Cryptocurrency.CryptocurrencyMap, new Dictionary<string, string>
                {
                    {"listing_status", listing_status.ToString() },
                    {"start", start.HasValue && start.Value >= 1 ? start.Value.ToString() : null },
                    {"limit", limit.HasValue&& limit.Value >= 1 ? limit.Value.ToString() : null },
                    {"symbol", symbol }
                });
            var response = await _httpClient.GetAsync(url);
            Pro.Model.Cryptocurrency.MapData data = await JsonParserService.ParseResponse<Pro.Model.Cryptocurrency.MapData>(response);
            if (data == null)
            {
                data = new Pro.Model.Cryptocurrency.MapData
                {
                    Data = null,
                    Status = new Status { ErrorCode = int.MinValue },
                    Success = false
                };
            }
            data.Success = data.Status.ErrorCode == 0;

            // Add to Status List
            this.statusList.Add(data.Status);

            return data;
        }

        /// <summary>
        /// Returns all static metadata for one or more cryptocurrencies including name, symbol, logo, and its various registered URLs.
        /// <para>At least one "id" or "symbol" is required.</para>
        /// </summary>
        /// <param name="id">
        /// One or more comma-separated CoinMarketCap cryptocurrency IDs. Example: "1,2"
        /// </param>
        /// <param name="symbol">
        /// Alternatively pass one or more comma-separated cryptocurrency symbols. Example: "BTC,ETH".
        /// </param>
        /// <returns></returns>
        public Pro.Model.Cryptocurrency.InfoData CryptocurrencyInfo(string id = null, string symbol = null)
        {
            return this.CryptocurrencyInfoAsync(id, symbol).Result;
        }

        public async Task<Pro.Model.Cryptocurrency.InfoData> CryptocurrencyInfoAsync(string id = null, string symbol = null)
        {
            HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(Pro.Config.Cryptocurrency.CoinMarketCapProApiUrl) };
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", this.ProApiKey);

            var url = QueryStringService.AppendQueryString(Pro.Config.Cryptocurrency.CryptocurrencyInfo, new Dictionary<string, string>
                {
                    {"id", id },
                    {"symbol", symbol }
                });
            var response = await _httpClient.GetAsync(url);
            Pro.Model.Cryptocurrency.InfoData data = await JsonParserService.ParseResponse<Pro.Model.Cryptocurrency.InfoData>(response);
            if (data == null)
            {
                data = new Pro.Model.Cryptocurrency.InfoData
                {
                    Data = null,
                    Status = new Status { ErrorCode = int.MinValue },
                    Success = false
                };
            }
            data.Success = data.Status.ErrorCode == 0;

            // Add to Status List
            this.statusList.Add(data.Status);

            return data;
        }


        /// <summary>
        /// Get a paginated list of all cryptocurrencies with market data for a given historical time. Use the "convert" option to return market values in multiple fiat and cryptocurrency conversions in the same call.
        /// <para>This endpoint is not yet available. It is slated for release in early Q4 2018.</para>
        /// </summary>
        /// <param name="timestamp">Timestamp (Unix or ISO 8601) to return historical cryptocurrency listings for.</param>
        /// <param name="start">>=1, Default=1. Optionally offset the start (1-based index) of the paginated list of items to return.</param>
        /// <param name="limit">[1 ... 5000] Default=100. Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size.</param>
        /// <param name="convert">Optionally calculate market quotes in up to 32 currencies at once by passing a comma-separated list of cryptocurrency or fiat currency symbols. Each additional convert option beyond the first requires an additional call credit. A list of supported fiat options can be found here. Each conversion is returned in its own "quote" object.</param>
        /// <param name="sort">What field to sort the list of cryptocurrencies by.</param>
        /// <param name="sort_dir">The direction in which to order cryptocurrencies against the specified sort.</param>
        /// <param name="cryptocurrency_type">The type of cryptocurrency to include.</param>
        /// <returns></returns>
        public Pro.Model.Cryptocurrency.ListingsData CryptocurrencyListingsHistorical(
            DateTime timestamp,
            int? start = 1,
            int? limit = 100,
            FiatCurrency convert = FiatCurrency.USD,
            SortBy sort = SortBy.market_cap,
            SortDirection sort_dir = SortDirection.desc,
            CryptocurrencyType cryptocurrency_type = CryptocurrencyType.all
            )
        {
            return this.CryptocurrencyListingsHistoricalAsync(timestamp, start, limit, convert, sort, sort_dir, cryptocurrency_type).Result;
        }

        public async Task<Pro.Model.Cryptocurrency.ListingsData> CryptocurrencyListingsHistoricalAsync(
            DateTime timestamp,
            int? start = 1,
            int? limit = 100,
            FiatCurrency convert = FiatCurrency.USD,
            SortBy sort = SortBy.market_cap,
            SortDirection sort_dir = SortDirection.desc,
            CryptocurrencyType cryptocurrency_type = CryptocurrencyType.all
            )
        {
            HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(Pro.Config.Cryptocurrency.CoinMarketCapProApiUrl) };
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", this.ProApiKey);

            var url = QueryStringService.AppendQueryString(Pro.Config.Cryptocurrency.CryptocurrencyListingsHistorical, new Dictionary<string, string>
                {
                    {"timestamp", timestamp.ToUnixTimeSeconds().ToString() },
                    {"start", start.HasValue && start.Value >= 1 ? start.Value.ToString() : null },
                    {"limit", limit.HasValue&& limit.Value >= 1 ? limit.Value.ToString() : null },
                    {"convert", convert.ToString() },
                    {"sort", sort.ToString() },
                    {"sort_dir", sort_dir.ToString() },
                    {"cryptocurrency_type", cryptocurrency_type.ToString() },
                });
            var response = await _httpClient.GetAsync(url);
            Pro.Model.Cryptocurrency.ListingsData data = await JsonParserService.ParseResponse<Pro.Model.Cryptocurrency.ListingsData>(response);
            if (data == null)
            {
                data = new Pro.Model.Cryptocurrency.ListingsData
                {
                    Data = null,
                    Status = new Status { ErrorCode = int.MinValue },
                    Success = false
                };
            }
            data.Success = data.Status.ErrorCode == 0;

            // Add to Status List
            this.statusList.Add(data.Status);

            return data;
        }

        /// <summary>
        /// Get a paginated list of all cryptocurrencies with latest market data.
        /// <para>You can configure this call to sort by market cap or another market ranking field. Use the "convert" option to return market values in multiple fiat and cryptocurrency conversions in the same call.</para>
        /// </summary>
        /// <param name="start">>=1, Default=1. Optionally offset the start (1-based index) of the paginated list of items to return.</param>
        /// <param name="limit">[1 ... 5000] Default=100. Optionally specify the number of results to return. Use this parameter and the "start" parameter to determine your own pagination size.</param>
        /// <param name="convert">Optionally calculate market quotes in up to 32 currencies at once by passing a comma-separated list of cryptocurrency or fiat currency symbols. Each additional convert option beyond the first requires an additional call credit. A list of supported fiat options can be found here. Each conversion is returned in its own "quote" object.</param>
        /// <param name="sort">What field to sort the list of cryptocurrencies by.</param>
        /// <param name="sort_dir">The direction in which to order cryptocurrencies against the specified sort.</param>
        /// <param name="cryptocurrency_type">The type of cryptocurrency to include.</param>
        /// <returns></returns>
        public Pro.Model.Cryptocurrency.ListingsData CryptocurrencyListingsLatest(
            int? start = 1,
            int? limit = 100,
            FiatCurrency convert = FiatCurrency.USD,
            SortBy sort = SortBy.market_cap,
            SortDirection sort_dir = SortDirection.desc,
            CryptocurrencyType cryptocurrency_type = CryptocurrencyType.all
            )
        {
            return this.CryptocurrencyListingsLatestAsync(start, limit, convert, sort, sort_dir, cryptocurrency_type).Result;
        }

        public async Task<Pro.Model.Cryptocurrency.ListingsData> CryptocurrencyListingsLatestAsync(
            int? start = 1,
            int? limit = 100,
            FiatCurrency convert = FiatCurrency.USD,
            SortBy sort = SortBy.market_cap,
            SortDirection sort_dir = SortDirection.desc,
            CryptocurrencyType cryptocurrency_type = CryptocurrencyType.all
            )
        {
            HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(Pro.Config.Cryptocurrency.CoinMarketCapProApiUrl) };
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", this.ProApiKey);

            var url = QueryStringService.AppendQueryString(Pro.Config.Cryptocurrency.CryptocurrencyListingsLatest, new Dictionary<string, string>
                {
                    {"start", start.HasValue && start.Value >= 1 ? start.Value.ToString() : null },
                    {"limit", limit.HasValue&& limit.Value >= 1 ? limit.Value.ToString() : null },
                    {"convert", convert.ToString() },
                    {"sort", sort.ToString() },
                    {"sort_dir", sort_dir.ToString() },
                    {"cryptocurrency_type", cryptocurrency_type.ToString() },
                });
            var response = await _httpClient.GetAsync(url);
            Pro.Model.Cryptocurrency.ListingsData data = await JsonParserService.ParseResponse<Pro.Model.Cryptocurrency.ListingsData>(response);
            if (data == null)
            {
                data = new Pro.Model.Cryptocurrency.ListingsData
                {
                    Data = null,
                    Status = new Status { ErrorCode = int.MinValue },
                    Success = false
                };
            }
            data.Success = data.Status.ErrorCode == 0;

            // Add to Status List
            this.statusList.Add(data.Status);

            return data;
        }

        /// <summary>
        /// Get the latest market quote for 1 or more cryptocurrencies. Use the "convert" option to return market values in multiple fiat and cryptocurrency conversions in the same call.
        /// <para>At least one "id" or "symbol" is required.</para>
        /// </summary>
        /// <param name="id">
        /// One or more comma-separated CoinMarketCap cryptocurrency IDs. Example: "1,2"
        /// </param>
        /// <param name="symbol">
        /// Alternatively pass one or more comma-separated cryptocurrency symbols. Example: "BTC,ETH".
        /// </param>
        /// <returns></returns>
        public Pro.Model.Cryptocurrency.QuotesData CryptocurrencyQuotesLatest(
            string id = null,
            string symbol = null,
            FiatCurrency convert = FiatCurrency.USD
            )
        {
            return this.CryptocurrencyQuotesLatestAsync(id, symbol, convert).Result;
        }

        public async Task<Pro.Model.Cryptocurrency.QuotesData> CryptocurrencyQuotesLatestAsync(
            string id = null,
            string symbol = null,
            FiatCurrency convert = FiatCurrency.USD
            )
        {
            HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(Pro.Config.Cryptocurrency.CoinMarketCapProApiUrl) };
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", this.ProApiKey);

            var url = QueryStringService.AppendQueryString(Pro.Config.Cryptocurrency.CryptocurrencyQuotesLatest, new Dictionary<string, string>
                {
                    {"id", id },
                    {"symbol", symbol },
                    {"convert", convert.ToString() },
                });
            var response = await _httpClient.GetAsync(url);
            Pro.Model.Cryptocurrency.QuotesData data = await JsonParserService.ParseResponse<Pro.Model.Cryptocurrency.QuotesData>(response);
            if (data == null)
            {
                data = new Pro.Model.Cryptocurrency.QuotesData
                {
                    Data = null,
                    Status = new Status { ErrorCode = int.MinValue },
                    Success = false
                };
            }
            data.Success = data.Status.ErrorCode == 0;

            // Add to Status List
            this.statusList.Add(data.Status);

            return data;
        }

        /// <summary>
        /// Get the latest quote of aggregate market metrics. Use the "convert" option to return market values in multiple fiat and cryptocurrency conversions in the same call.
        /// </summary>
        /// <param name="convert">Optionally calculate market quotes in up to 32 currencies at once by passing a comma-separated list of cryptocurrency or fiat currency symbols. Each additional convert option beyond the first requires an additional call credit. A list of supported fiat options can be found here. Each conversion is returned in its own "quote" object.</param>
        /// <returns></returns>
        public CoinMarketCap.Pro.Model.GlobalMetrics.QuotesData GlobalMetricsQuotesLatest(FiatCurrency convert = FiatCurrency.USD)
        {
            return this.GlobalMetricsQuotesLatestAsync(convert).Result;
        }

        public async Task<CoinMarketCap.Pro.Model.GlobalMetrics.QuotesData> GlobalMetricsQuotesLatestAsync(FiatCurrency convert = FiatCurrency.USD)
        {
            HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(Pro.Config.GlobalMetrics.CoinMarketCapProApiUrl) };
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", this.ProApiKey);

            var url = QueryStringService.AppendQueryString(Pro.Config.GlobalMetrics.GlobalMetricsLatest, new Dictionary<string, string>
            {
                {"convert", convert.ToString() },
            });
            var response = await _httpClient.GetAsync(url);
            CoinMarketCap.Pro.Model.GlobalMetrics.QuotesData data = await JsonParserService.ParseResponse<CoinMarketCap.Pro.Model.GlobalMetrics.QuotesData>(response);
            if (data == null)
            {
                data = new CoinMarketCap.Pro.Model.GlobalMetrics.QuotesData
                {
                    Data = null,
                    Status = new Status { ErrorCode = int.MinValue },
                    Success = false
                };
            }
            data.Success = data.Status.ErrorCode == 0;

            // Add to Status List
            this.statusList.Add(data.Status);

            return data;
        }
    }
}
