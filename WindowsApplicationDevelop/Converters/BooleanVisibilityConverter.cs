using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace WindowsApplicationDevelop.Converters
{
    public sealed class BooleanVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a Visibility");

            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return ((Visibility)value) switch
            {
                Visibility.Visible => true,
                Visibility.Collapsed => false,
                _ => throw new NotSupportedException()
            };
        }
    }
}
