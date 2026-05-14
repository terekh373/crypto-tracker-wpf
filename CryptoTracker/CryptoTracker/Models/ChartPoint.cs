using System.Text.Json.Serialization;

namespace CryptoTracker.Models
{
    public class MarketChartResponse
    {
        [JsonPropertyName("prices")]
        public List<List<double>> Prices { get; set; } = new();
    }

    public class ChartPoint
    {
        public DateTime Date { get; set; }
        public double Price { get; set; }
    }
}