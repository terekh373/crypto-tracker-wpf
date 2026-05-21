using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoTracker.Models;
using CryptoTracker.Services;
using CryptoTracker.Views;
using System.Collections.ObjectModel;

namespace CryptoTracker.ViewModels
{
    public partial class CoinsListViewModel : ObservableObject
    {
        private readonly CoinGeckoService _coinService;

        [ObservableProperty]
        private ObservableCollection<CoinMarket> _coins = new();

        [ObservableProperty]
        private bool _isLoading = false;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        [ObservableProperty]
        private bool _hasError = false;

        public CoinsListViewModel()
        {
            _coinService = ServiceLocator.CoinGeckoService;
        }

        [RelayCommand]
        public async Task LoadCoinsAsync()
        {
            IsLoading = true;
            HasError = false;
            ErrorMessage = string.Empty;

            try
            {
                var coins = await _coinService.GetTopCoinsAsync(count: 10);
                Coins = new ObservableCollection<CoinMarket>(coins);
            }
            catch
            {
                HasError = true;
                ErrorMessage = "Failed to load data. Check your internet connection and try again.";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public void OpenCoinDetail(CoinMarket coin)
        {
            if (coin == null) return;
            ServiceLocator.NavigationService.NavigateTo<CoinDetailPage>(coin.Id);
        }
    }
}