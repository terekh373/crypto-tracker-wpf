using CryptoTracker.ViewModels;
using System.Windows.Controls;

namespace CryptoTracker.Views
{
    public partial class CurrencyConverterPage : Page
    {
        private readonly ConverterViewModel _viewModel;

        public CurrencyConverterPage()
        {
            InitializeComponent();
            _viewModel = new ConverterViewModel();
            DataContext = _viewModel;
            Loaded += async (s, e) => await _viewModel.LoadAsync();
        }
    }
}