using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CryptoTracker.Converters
{
    // Converts bool to Visibility: true = Visible, false = Collapsed
    // Pass parameter="Inverse" to flip the logic: true = Collapsed, false = Visible
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = value is bool b && b;

            if (parameter is string s && s == "Inverse") flag = !flag;

            return flag ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}