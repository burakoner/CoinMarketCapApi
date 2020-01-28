namespace CoinMarketCap.Pro.Config
{
    public static class Cryptocurrency
    {
        public static string CoinMarketCapProApiUrl => "https://pro-api.coinmarketcap.com/v1/";
        public static string CryptocurrencyInfo => "cryptocurrency/info";
        public static string CryptocurrencyMap => "cryptocurrency/map";
        public static string CryptocurrencyListingsLatest => "cryptocurrency/listings/latest";
        public static string CryptocurrencyListingsHistorical => "cryptocurrency/listings/historical";
        public static string CryptocurrencyMarketPairsLatest => "cryptocurrency/market-pairs/latest";
        public static string CryptocurrencyOHLCVHistorical => "cryptocurrency/ohlcv/historical";
        public static string CryptocurrencyQuotesLatest => "cryptocurrency/quotes/latest";
        public static string CryptocurrencyQuotesHistorical => "cryptocurrency/quotes/historical";
    }
}