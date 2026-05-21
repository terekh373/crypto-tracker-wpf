using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoTracker.Models;
using CryptoTracker.Services;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace CryptoTracker.ViewModels
{
    public partial class CoinDetailViewModel : ObservableObject
    {
        private readonly CoinGeckoService _coinService;
        private readonly string _coinId;

        [ObservableProperty]
        private CoinDetail? _coin;

        [ObservableProperty]
        private bool _isLoading = false;

        [ObservableProperty]
        private bool _hasError = false;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        [ObservableProperty]
        private int _selectedDays = 7;

        [ObservableProperty]
        private ISeries[] _series = Array.Empty<ISeries>();

        [ObservableProperty]
        private Axis[] _xAxes = new Axis[]
        {
            new Axis { Labels = new List<string>(), LabelsPaint = new SolidColorPaint(SKColors.Gray), TextSize = 11 }
        };

        [ObservableProperty]
        private Axis[] _yAxes = new Axis[]
        {
            new Axis { LabelsPaint = new SolidColorPaint(SKColors.Gray), TextSize = 11, Labeler = val => $"${val:N0}" }
        };

        public ObservableCollection<Ticker> Markets { get; } = new();

        public CoinDetailViewModel(string coinId)
        {
            _coinId = coinId;
            _coinService = ServiceLocator.CoinGeckoService;
        }

        public async Task LoadAsync()
        {
            IsLoading = true;
            HasError = false;

            try
            {
                var detail = await _coinService.GetCoinDetailAsync(_coinId);
                Coin = detail;

                Markets.Clear();
                foreach (var ticker in detail.Tickers.Take(10))
                    Markets.Add(ticker);

                await LoadChartAsync(SelectedDays);
            }
            catch
            {
                HasError = true;
                ErrorMessage = "Failed to load coin details.";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task ChangePeriodAsync(string days)
        {
            if (int.TryParse(days, out var d))
            {
                SelectedDays = d;
                await LoadChartAsync(d);
            }
        }

        [RelayCommand]
        public void GoBack()
        {
            ServiceLocator.NavigationService.GoBack();
        }

        private async Task LoadChartAsync(int days)
        {
            try
            {
                var points = await _coinService.GetMarketChartAsync(_coinId, days);

                var values = points.Select(p => new ObservablePoint(
                    p.Date.ToOADate(),
                    p.Price
                )).ToList();

                Series = new ISeries[]
                {
                    new LineSeries<ObservablePoint>
                    {
                        Values = values,
                        Fill = null,
                        GeometrySize = 0,
                        Stroke = new SolidColorPaint(SKColor.Parse("#F7931A")) { StrokeThickness = 2 },
                        Name = "Price"
                    }
                };

                var labels = points.Select(p =>
                    days <= 1 ? p.Date.ToString("HH:mm") :
                    days <= 30 ? p.Date.ToString("MMM dd") :
                    p.Date.ToString("MMM yyyy")).ToList();

                var step = Math.Max(1, labels.Count / 6);
                var sparseLabels = labels
                    .Select((l, i) => i % step == 0 ? l : "")
                    .ToList();

                XAxes = new Axis[]
                {
                    new Axis
                    {
                        Labels = sparseLabels,
                        LabelsPaint = new SolidColorPaint(SKColors.Gray),
                        TextSize = 11
                    }
                };
            }
            catch { }
        }
    }
}
