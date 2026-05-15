using System.Windows.Controls;

namespace CryptoTracker.Services
{
    public class NavigationService : INavigationService
    {
        private Frame? _frame;

        public void SetFrame(Frame frame)
        {
            _frame = frame;
        }

        public void NavigateTo<TPage>(object? parameter = null) where TPage : class
        {
            if (_frame == null) throw new InvalidOperationException("Frame is not set. Call SetFrame first.");

            var page = Activator.CreateInstance(typeof(TPage), parameter) as Page ?? Activator.CreateInstance(typeof(TPage)) as Page;

            if (page != null) _frame.Navigate(page);
        }

        public void GoBack()
        {
            if (_frame?.CanGoBack == true) _frame.GoBack();
        }

        public bool CanGoBack => _frame?.CanGoBack ?? false;
    }
}