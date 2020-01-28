using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinMarketCap.Public.Config.Parameters;
using CoinMarketCap.Public.Model;
using CoinMarketCap.Public.Parameters;
using CoinMarketCap.Shared.Services;

namespace CoinMarketCap
{
    /// <summary>
    /// Coin Market Cap Api Client
    /// </summary>
    public class PublicClient : IDisposable
    {

        public PublicClient()
        {
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

        public GlobalData Global(FiatCurrency convert = FiatCurrency.USD)
        {
            return this.GlobalAsync(convert).Result;
        }

        public async Task<GlobalData> GlobalAsync(FiatCurrency convert = FiatCurrency.USD)
        {
            GlobalData data = new GlobalData();
            try
            {
                HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(Endpoints.CoinMarketCapApiUrl) };
                var url = QueryStringService.AppendQueryString(Endpoints.GlobalData, new Dictionary<string, string>
                {
                    { "convert", convert.ToString()}
                });
                var response = await _httpClient.GetAsync(url);
                data = await JsonParserService.ParseResponse<GlobalData>(response);
                if (data == null)
                {
                    data = new GlobalData
                    {
                        Data = null,
                        Metadata = new GlobalMetadata { Error = int.MinValue },
                        Success = false
                    };
                }
                data.Success = data.Metadata.Error == null;
            }
            catch { }

            return data;
        }

        public ListingsData Listings()
        {
            return this.ListingsAsync().Result;
        }

        public async Task<ListingsData> ListingsAsync()
        {
            ListingsData data = new ListingsData();
            try
            {
                HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(Endpoints.CoinMarketCapApiUrl) };
                var url = QueryStringService.AppendQueryString(Endpoints.Listings, new Dictionary<string, string>());
                var response = await _httpClient.GetAsync(url);
                data = await JsonParserService.ParseResponse<ListingsData>(response);
                if (data == null)
                {
                    data = new ListingsData
                    {
                        Data = null,
                        Metadata = new ListingsMetadata { Error = int.MinValue },
                        Success = false
                    };
                }
                data.Success = data.Metadata.Error == null; ;
            }
            catch { }

            return data;
        }

        public TickersData Tickers()
        {
            return this.TickersAsync().Result;
        }

        public async Task<TickersData> TickersAsync()
        {
            TickersData data = new TickersData();
            try
            {
                HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(Endpoints.CoinMarketCapApiUrl) };
                var url = QueryStringService.AppendQueryString(Endpoints.Ticker, new Dictionary<string, string>());
                var response = await _httpClient.GetAsync(url);
                data = await JsonParserService.ParseResponse<TickersData>(response);
                if (data == null)
                {
                    data = new TickersData
                    {
                        Data = null,
                        Metadata = new TickerMetadata { Error = int.MinValue },
                        Success = false
                    };
                }
                data.Success = data.Metadata.Error == null;
            }
            catch { }

            return data;
        }

        public TickersData Tickers(int? start, int? limit, SortBy sort = SortBy.id, FiatCurrency convert = FiatCurrency.USD)
        {
            return this.TickersAsync(start, limit, sort, convert).Result;
        }

        public async Task<TickersData> TickersAsync(int? start, int? limit, SortBy sort = SortBy.id, FiatCurrency convert = FiatCurrency.USD)
        {
            TickersData data = new TickersData();
            try
            {
                HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(Endpoints.CoinMarketCapApiUrl) };
                var url = QueryStringService.AppendQueryString(Endpoints.Ticker, new Dictionary<string, string>
                {
                    {"start",start >= 1 ? start.ToString() : null },
                    {"limit",limit >= 1 ? limit.ToString() : null },
                    {"sort",sort.ToString() },
                    {"convert",convert.ToString() }
                });
                var response = await _httpClient.GetAsync(url);
                data = await JsonParserService.ParseResponse<TickersData>(response);
                if (data == null)
                {
                    data = new TickersData
                    {
                        Data = null,
                        Metadata = new TickerMetadata { Error = int.MinValue },
                        Success = false
                    };
                }
                data.Success = data.Metadata.Error == null;
            }
            catch { }

            return data;
        }
        
        public TickerByIdData TickerById(int id, FiatCurrency convert = FiatCurrency.USD)
        {
            return this.TickerByIdAsync(id, convert).Result;
        }

        public async Task<TickerByIdData> TickerByIdAsync(int id, FiatCurrency convert = FiatCurrency.USD)
        {
            TickerByIdData data = new TickerByIdData();
            try
            {
                HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(Endpoints.CoinMarketCapApiUrl) };
                var url = QueryStringService.AppendQueryString($"{Endpoints.Ticker}/{id}", new Dictionary<string, string>
                {
                    {"convert",convert.ToString() }
                });
                var response = await _httpClient.GetAsync(url);
                data = await JsonParserService.ParseResponse<TickerByIdData>(response);
                if (data == null)
                {
                    data = new TickerByIdData
                    {
                        Data = null,
                        Metadata = new TickerMetadata { Error = int.MinValue },
                        Success = false
                    };
                }
                data.Success = data.Metadata.Error == null;
            }
            catch { }

            return data;
        }

    }
}
