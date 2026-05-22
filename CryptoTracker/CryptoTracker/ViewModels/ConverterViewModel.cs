using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoTracker.Services;
using System.Collections.ObjectModel;

namespace CryptoTracker.ViewModels
{
    public partial class ConverterViewModel : ObservableObject
    {
        private readonly CoinGeckoService _coinService;

        private readonly Dictionary<string, decimal> _pricesInUsd = new();

        [ObservableProperty]
        private ObservableCollection<string> _availableCoins = new();

        [ObservableProperty]
        private string _selectedFromCoin = "bitcoin";

        [ObservableProperty]
        private string _selectedToCoin = "ethereum";

        [ObservableProperty]
        private string _amountInput = "1";

        [ObservableProperty]
        private string _convertedResult = string.Empty;

        [ObservableProperty]
        private string _rateInfo = string.Empty;

        [ObservableProperty]
        private bool _isLoading = false;

        [ObservableProperty]
        private bool _hasError = false;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        public ConverterViewModel()
        {
            _coinService = ServiceLocator.CoinGeckoService;
        }

        public async Task LoadAsync()
        {
            IsLoading = true;
            HasError = false;

            try
            {
                var coins = await _coinService.GetTopCoinsAsync(count: 20);

                AvailableCoins.Clear();
                _pricesInUsd.Clear();

                foreach (var coin in coins)
                {
                    AvailableCoins.Add(coin.Id);
                    _pricesInUsd[coin.Id] = coin.CurrentPrice;
                }

                Convert();
            }
            catch
            {
                HasError = true;
                ErrorMessage = "Failed to load prices. Check your internet connection.";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public void Convert()
        {
            if (!decimal.TryParse(AmountInput.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out var amount))
            {
                ConvertedResult = "Invalid amount";
                RateInfo = string.Empty;
                return;
            }

            if (!_pricesInUsd.TryGetValue(SelectedFromCoin, out var fromPrice) ||
                !_pricesInUsd.TryGetValue(SelectedToCoin, out var toPrice) ||
                toPrice == 0)
            {
                ConvertedResult = "Price data unavailable";
                RateInfo = string.Empty;
                return;
            }

            var amountInUsd = amount * fromPrice;
            var result = amountInUsd / toPrice;

            ConvertedResult = $"{result:N6} {SelectedToCoin.ToUpper()}";
            RateInfo = $"1 {SelectedFromCoin.ToUpper()} = {fromPrice / toPrice:N6} {SelectedToCoin.ToUpper()}";
        }

        [RelayCommand]
        public void SwapCoins()
        {
            (SelectedFromCoin, SelectedToCoin) = (SelectedToCoin, SelectedFromCoin);
            Convert();
        }

        [RelayCommand]
        public void GoBack()
        {
            ServiceLocator.NavigationService.GoBack();
        }
    }
}