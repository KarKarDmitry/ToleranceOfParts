using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PDB_TEST.ResourceDictionaries.Converters
{
    public class ZeroToCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue && intValue == 0)
            {
                return Visibility.Collapsed;
            }
            if (value is double doubleValue && doubleValue == 0)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


