# ₿ CryptoTracker

A desktop cryptocurrency tracker built with WPF and .NET 8, using the CoinGecko public API.

![Platform](https://img.shields.io/badge/platform-Windows-blue)
![Framework](https://img.shields.io/badge/.NET-8.0-purple)
![Language](https://img.shields.io/badge/language-C%23-239120)
![Architecture](https://img.shields.io/badge/architecture-MVVM-orange)

---

## Features

- **Top 10 coins** — ranked by market capitalization with live prices, 24h change and volume
- **Coin detail page** — full stats, price chart with 1D / 7D / 30D / 1Y periods and top 10 markets
- **Search** — find any cryptocurrency by name or ticker symbol
- **Currency converter** — convert between top 20 cryptocurrencies in real time
- **Dark / Light theme** — toggle at any time without restarting the app
- **Error handling** — graceful messages for network issues and API rate limits

---

## Screenshots

<img width="1079" height="692" alt="main" src="https://github.com/user-attachments/assets/9eece163-a69c-471c-af86-45495e3f98f1" />
<img width="882" height="1030" alt="details" src="https://github.com/user-attachments/assets/9aafa978-fa54-4516-a825-d81511144fea" />
<img width="878" height="685" alt="converter" src="https://github.com/user-attachments/assets/50c6ca42-7b36-44ba-bb93-5ed4e869f87a" />
<img width="1508" height="692" alt="search2" src="https://github.com/user-attachments/assets/17b26d6d-3b35-4439-913b-ce7838fc1c56" />
<img width="877" height="1027" alt="search" src="https://github.com/user-attachments/assets/12df80a5-6dde-4d1d-8379-1dbe2a35944a" />

---

## Tech Stack

| Layer | Technology |
|---|---|
| Language | C# 12 |
| Framework | .NET 8 / WPF |
| Architecture | MVVM |
| MVVM Toolkit | CommunityToolkit.Mvvm |
| Charts | LiveChartsCore.SkiaSharp.WPF |
| HTTP | HttpClient (System.Net.Http) |
| JSON | System.Text.Json |
| API | CoinGecko Public API v3 |

---

## Project Structure

```
CryptoTracker/
├── Models/              # Data classes mapped from API responses
│   ├── CoinMarket.cs    # Top coins list item
│   ├── CoinDetail.cs    # Full coin info with market data and tickers
│   ├── SearchResult.cs  # Search response wrapper
│   └── ChartPoint.cs    # Price history point for chart
│
├── ViewModels/          # Business logic, one ViewModel per page
│   ├── CoinsListViewModel.cs
│   ├── CoinDetailViewModel.cs
│   ├── SearchViewModel.cs
│   └── ConverterViewModel.cs
│
├── Views/               # XAML pages and windows
│   ├── MainWindow.xaml  # Shell with top nav bar and Frame
│   ├── CoinsListPage.xaml
│   ├── CoinDetailPage.xaml
│   ├── SearchPage.xaml
│   └── CurrencyConverterPage.xaml
│
├── Services/            # Infrastructure and cross-cutting concerns
│   ├── CoinGeckoService.cs   # All API calls via HttpClient
│   ├── NavigationService.cs  # Frame-based page navigation
│   ├── ThemeManager.cs       # Runtime theme switching
│   └── ServiceLocator.cs     # Simple static DI container
│
├── Converters/          # IValueConverter implementations for XAML bindings
│   ├── PriceChangeColorConverter.cs   # double → green/red brush
│   ├── PriceChangeSignConverter.cs    # double → "+2.45%" string
│   └── BoolToVisibilityConverter.cs  # bool → Visibility (supports Inverse)
│
└── Themes/              # ResourceDictionary files
    ├── DarkTheme.xaml
    └── LightTheme.xaml
```

---

## Getting Started

### Prerequisites

- Windows 10 or later
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (Community or higher) with the **.NET desktop development** workload

### Run locally

```bash
git clone https://github.com/terekh373/crypto-tracker-wpf.git
cd crypto-tracker-wpf
```

Open `CryptoTracker/CryptoTracker.sln` in Visual Studio, then press **F5**.

NuGet packages are restored automatically on first build.

---

## Architecture

The app follows the **MVVM** pattern:

```
View (XAML)
  │  DataContext binding
  ▼
ViewModel (ObservableObject)
  │  async calls
  ▼
Service (CoinGeckoService)
  │  HttpClient
  ▼
CoinGecko API
```

- **Views** contain zero business logic — only XAML bindings and event handlers that delegate to ViewModel commands
- **ViewModels** expose `[ObservableProperty]` fields and `[RelayCommand]` methods via CommunityToolkit.Mvvm source generators
- **Services** are instantiated once as singletons through `ServiceLocator` and shared across all ViewModels
- **Navigation** is handled by `NavigationService` which wraps WPF's `Frame` and uses reflection to instantiate pages with optional constructor parameters

---

## API

Data is provided by the [CoinGecko Public API v3](https://www.coingecko.com/en/api). No API key is required.

| Endpoint | Used for |
|---|---|
| `/coins/markets` | Top coins list, converter prices |
| `/coins/{id}` | Coin detail, tickers |
| `/coins/{id}/market_chart` | Price history for chart |
| `/search` | Coin search |

> The free tier allows approximately 10–30 requests per minute. The app handles HTTP 429 responses gracefully.

---

## License

Feel free to use it as a reference.
