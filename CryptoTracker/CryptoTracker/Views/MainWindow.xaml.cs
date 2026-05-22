using System.Windows;
using System.Windows.Input;
using CryptoTracker.Services;

namespace CryptoTracker.Views
{
    public partial class MainWindow : Window
    {
        private bool _isDarkTheme = true;

        public MainWindow()
        {
            InitializeComponent();
            ServiceLocator.NavigationService.SetFrame(MainFrame);
            ServiceLocator.NavigationService.NavigateTo<CoinsListPage>();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ServiceLocator.NavigationService.NavigateTo<CoinsListPage>();
        }

        private void ConverterButton_Click(object sender, RoutedEventArgs e)
        {
            ServiceLocator.NavigationService.NavigateTo<CurrencyConverterPage>();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteSearch();
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ExecuteSearch();
        }

        private void ExecuteSearch()
        {
            var query = SearchBox.Text.Trim();
            if (!string.IsNullOrEmpty(query))
                ServiceLocator.NavigationService.NavigateTo<SearchPage>(query);
        }

        private void ThemeToggleButton_Click(object sender, RoutedEventArgs e)
        {
            _isDarkTheme = !_isDarkTheme;
            ThemeManager.ApplyTheme(_isDarkTheme ? "Dark" : "Light");
            ThemeToggleButton.Content = _isDarkTheme ? "🌙" : "☀️";
        }
    }
}