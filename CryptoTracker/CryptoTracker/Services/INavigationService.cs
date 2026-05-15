namespace CryptoTracker.Services
{
    public interface INavigationService
    {
        void NavigateTo<TPage>(object? parameter = null) where TPage : class;
        void GoBack();
        bool CanGoBack { get; }
    }
}
