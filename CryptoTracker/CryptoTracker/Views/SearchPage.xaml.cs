using CryptoTracker.ViewModels;
using System.Windows.Controls;

namespace CryptoTracker.Views
{
    public partial class SearchPage : Page
    {
        private readonly SearchViewModel _viewModel;

        public SearchPage(string query)
        {
            InitializeComponent();
            _viewModel = new SearchViewModel();
            DataContext = _viewModel;
            Loaded += async (s, e) => await _viewModel.SearchAsync(query);
        }

        public SearchPage() : this(string.Empty) { }
    }
}
