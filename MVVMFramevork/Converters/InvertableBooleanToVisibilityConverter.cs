using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MVVMFramevork
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class InvertableBooleanToVisibilityConverter : IValueConverter
    {
        public enum ConvetrerParameters
        {
            Normal, Inverted
        }
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            var direction = (ConvetrerParameters)Enum.Parse(typeof(ConvetrerParameters), (string)parameter);

            if (direction == ConvetrerParameters.Inverted)
                return !boolValue ? Visibility.Visible : Visibility.Hidden;

            return boolValue ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}