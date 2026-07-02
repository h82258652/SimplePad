using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace SimplePad.Tabs;

internal sealed partial class InversedVisibilityConverter : IValueConverter
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
