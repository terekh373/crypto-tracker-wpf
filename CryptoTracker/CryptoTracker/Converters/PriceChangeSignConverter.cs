using System.Globalization;
using System.Windows.Data;

namespace CryptoTracker.Converters
{
    // Adds + sign to positive numbers and formats to 2 decimal places with % symbol
    public class PriceChangeSignConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d) return d >= 0 ? $"+{d:F2}%" : $"{d:F2}%";

            return "0.00%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}