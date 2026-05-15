using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CryptoTracker.Converters
{
    // Returns green brush for positive values, red for negative, gray for zero
    public class PriceChangeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d)
            {
                if (d > 0) return new SolidColorBrush(Color.FromRgb(34, 197, 94));
                if (d < 0) return new SolidColorBrush(Color.FromRgb(239, 68, 68));
            }
            return new SolidColorBrush(Color.FromRgb(160, 160, 160));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}