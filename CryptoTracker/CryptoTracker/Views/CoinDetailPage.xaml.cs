using CryptoTracker.ViewModels;
using System.Windows.Controls;

namespace CryptoTracker.Views
{
    public partial class CoinDetailPage : Page
    {
        private readonly CoinDetailViewModel _viewModel;

        public CoinDetailPage(string coinId)
        {
            InitializeComponent();
            _viewModel = new CoinDetailViewModel(coinId);
            DataContext = _viewModel;
            Loaded += async (s, e) => await _viewModel.LoadAsync();
        }

        public CoinDetailPage() : this("bitcoin") { }
    }
}
