using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ToleranceOfParts.Views.Pages.Guides.Permissions
{
    public class IsEditableToEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Проверяем значение isEditable, если false, возвращаем Collapsed, иначе Visible
            bool isEditable = value != null && (bool)value;
            return isEditable ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Не используется в данном контексте
            return value;
        }
    }
}
