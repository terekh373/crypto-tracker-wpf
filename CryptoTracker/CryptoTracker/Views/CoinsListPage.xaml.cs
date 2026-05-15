using CryptoTracker.ViewModels;
using System.Windows.Controls;

namespace CryptoTracker.Views
{
    public partial class CoinsListPage : Page
    {
        private readonly CoinsListViewModel _viewModel;

        public CoinsListPage()
        {
            InitializeComponent();
            _viewModel = new CoinsListViewModel();
            DataContext = _viewModel;
            Loaded += async (s, e) => await _viewModel.LoadCoinsAsync();
        }
    }
}
