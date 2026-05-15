using System.Windows;

namespace CryptoTracker.Services
{
    public static class ThemeManager
    {
        private const string DarkThemeSource = "Themes/DarkTheme.xaml";
        private const string LightThemeSource = "Themes/LightTheme.xaml";

        public static void ApplyTheme(string themeName)
        {
            var newSource = themeName == "Dark" ? DarkThemeSource : LightThemeSource;
            var newUri = new Uri(newSource, UriKind.Relative);

            var mergedDicts = Application.Current.Resources.MergedDictionaries;

            var existing = mergedDicts.FirstOrDefault(d => d.Source != null && (d.Source.OriginalString.Contains("DarkTheme") || d.Source.OriginalString.Contains("LightTheme")));

            if (existing != null) mergedDicts.Remove(existing);

            mergedDicts.Add(new ResourceDictionary { Source = newUri });
        }
    }
}
