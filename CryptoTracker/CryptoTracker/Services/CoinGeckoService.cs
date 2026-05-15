using CryptoTracker.Models;
using System.Net.Http;
using System.Text.Json;

namespace CryptoTracker.Services
{
    public class CoinGeckoService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://api.coingecko.com/api/v3";

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public CoinGeckoService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CryptoTracker/1.0");
        }

        public async Task<List<CoinMarket>> GetTopCoinsAsync(int count = 10, string currency = "usd")
        {
            var url = $"{BaseUrl}/coins/markets?vs_currency={currency}&order=market_cap_desc&per_page={count}&page=1&sparkline=false";
            var json = await GetStringAsync(url);
            return JsonSerializer.Deserialize<List<CoinMarket>>(json, JsonOptions) ?? new List<CoinMarket>();
        }

        public async Task<CoinDetail> GetCoinDetailAsync(string coinId)
        {
            var url = $"{BaseUrl}/coins/{coinId}?localization=false&tickers=true&market_data=true&community_data=false&developer_data=false";
            var json = await GetStringAsync(url);
            return JsonSerializer.Deserialize<CoinDetail>(json, JsonOptions) ?? new CoinDetail();
        }

        public async Task<List<ChartPoint>> GetMarketChartAsync(string coinId, int days = 7, string currency = "usd")
        {
            var url = $"{BaseUrl}/coins/{coinId}/market_chart?vs_currency={currency}&days={days}";
            var json = await GetStringAsync(url);

            var response = JsonSerializer.Deserialize<MarketChartResponse>(json, JsonOptions);
            if (response == null) return new List<ChartPoint>();

            return response.Prices.Select(p => new ChartPoint
            {
                Date = DateTimeOffset.FromUnixTimeMilliseconds((long)p[0]).LocalDateTime,
                Price = p[1]
            }).ToList();
        }

        public async Task<List<SearchCoin>> SearchCoinsAsync(string query)
        {
            var url = $"{BaseUrl}/search?query={Uri.EscapeDataString(query)}";
            var json = await GetStringAsync(url);
            var response = JsonSerializer.Deserialize<SearchResponse>(json, JsonOptions);
            return response?.Coins ?? new List<SearchCoin>();
        }

        public async Task<Dictionary<string, decimal>> GetSupportedCurrenciesRatesAsync(string baseCoin = "bitcoin")
        {
            var url = $"{BaseUrl}/coins/{baseCoin}?localization=false&tickers=false&market_data=true&community_data=false&developer_data=false";
            var json = await GetStringAsync(url);
            var detail = JsonSerializer.Deserialize<CoinDetail>(json, JsonOptions);
            return detail?.MarketData?.CurrentPrice ?? new Dictionary<string, decimal>();
        }

        private async Task<string> GetStringAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}