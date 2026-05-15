namespace CryptoTracker.Services
{
    // Simple service locator that provides a single access point to all services.
    // In production apps we would use a proper DI container, but for this project a static locator is clean and sufficient.
    public static class ServiceLocator
    {
        public static CoinGeckoService CoinGeckoService { get; } = new CoinGeckoService();
        public static NavigationService NavigationService { get; } = new NavigationService();
    }
}