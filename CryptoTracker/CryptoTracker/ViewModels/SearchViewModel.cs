using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoTracker.Models;
using CryptoTracker.Services;
using CryptoTracker.Views;
using System.Collections.ObjectModel;

namespace CryptoTracker.ViewModels
{
    public partial class SearchViewModel : ObservableObject
    {
        private readonly CoinGeckoService _coinService;

        [ObservableProperty]
        private ObservableCollection<SearchCoin> _results = new();

        [ObservableProperty]
        private bool _isLoading = false;

        [ObservableProperty]
        private bool _hasError = false;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        [ObservableProperty]
        private bool _hasResults = false;

        [ObservableProperty]
        private string _query = string.Empty;

        public SearchViewModel()
        {
            _coinService = ServiceLocator.CoinGeckoService;
        }

        public async Task SearchAsync(string query)
        {
            Query = query;
            IsLoading = true;
            HasError = false;
            HasResults = false;
            Results.Clear();

            try
            {
                var coins = await _coinService.SearchCoinsAsync(query);
                foreach (var coin in coins)
                    Results.Add(coin);

                HasResults = Results.Count > 0;
            }
            catch
            {
                HasError = true;
                ErrorMessage = "Search failed. Check your internet connection and try again.";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public void OpenCoinDetail(SearchCoin coin)
        {
            if (coin == null) return;
            ServiceLocator.NavigationService.NavigateTo<CoinDetailPage>(coin.Id);
        }

        [RelayCommand]
        public void GoBack()
        {
            ServiceLocator.NavigationService.GoBack();
        }
    }
}
