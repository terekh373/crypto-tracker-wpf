using System.Text.Json.Serialization;

namespace CryptoTracker.Models
{
    public class CoinDetail
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public CoinImage? Image { get; set; }

        [JsonPropertyName("description")]
        public CoinDescription? Description { get; set; }

        [JsonPropertyName("market_cap_rank")]
        public int MarketCapRank { get; set; }

        [JsonPropertyName("market_data")]
        public MarketData? MarketData { get; set; }

        [JsonPropertyName("tickers")]
        public List<Ticker> Tickers { get; set; } = new();
    }

    public class CoinImage
    {
        [JsonPropertyName("large")]
        public string Large { get; set; } = string.Empty;
    }

    public class CoinDescription
    {
        [JsonPropertyName("en")]
        public string En { get; set; } = string.Empty;
    }

    public class MarketData
    {
        [JsonPropertyName("current_price")]
        public Dictionary<string, decimal> CurrentPrice { get; set; } = new();

        [JsonPropertyName("market_cap")]
        public Dictionary<string, decimal> MarketCap { get; set; } = new();

        [JsonPropertyName("total_volume")]
        public Dictionary<string, decimal> TotalVolume { get; set; } = new();

        [JsonPropertyName("price_change_percentage_24h")]
        public double PriceChangePercentage24h { get; set; }

        [JsonPropertyName("price_change_percentage_7d")]
        public double PriceChangePercentage7d { get; set; }

        [JsonPropertyName("price_change_percentage_30d")]
        public double PriceChangePercentage30d { get; set; }

        [JsonPropertyName("high_24h")]
        public Dictionary<string, decimal> High24h { get; set; } = new();

        [JsonPropertyName("low_24h")]
        public Dictionary<string, decimal> Low24h { get; set; } = new();

        [JsonPropertyName("ath")]
        public Dictionary<string, decimal> Ath { get; set; } = new();
    }

    public class Ticker
    {
        [JsonPropertyName("market")]
        public TickerMarket? Market { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; } = string.Empty;

        [JsonPropertyName("target")]
        public string Target { get; set; } = string.Empty;

        [JsonPropertyName("last")]
        public decimal Last { get; set; }

        [JsonPropertyName("trade_url")]
        public string? TradeUrl { get; set; }
    }

    public class TickerMarket
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("identifier")]
        public string Identifier { get; set; } = string.Empty;
    }
}