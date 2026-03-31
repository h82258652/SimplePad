using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SimplePad.Core.UWP.Converters;

public sealed partial class InversedVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        Visibility visibility = (Visibility)value;
        return visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        Visibility visibility = (Visibility)value;
        return visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
    }
}
