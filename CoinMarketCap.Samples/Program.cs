using CoinMarketCap.Pro.Parameters;
using System;

namespace CoinMarketCap.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var cli = new CoinMarketCap.ProClient("******");
            var map = cli.CryptocurrencyMap(listing_status: ListingStatus.active, 1, 2);

            Console.WriteLine("Hello World!");
        }
    }
}
