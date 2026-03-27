using System;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace SimplePad.UWP.UI.Controls;

public sealed partial class AppTabViewItem : TabViewItem
{
    public static readonly DependencyProperty IsModifiedProperty = DependencyProperty.Register(
        nameof(IsModified),
        typeof(bool),
        typeof(AppTabViewItem),
        new PropertyMetadata(false)
    );

    public AppTabViewItem()
    {
        DefaultStyleKey = typeof(AppTabViewItem);
        DefaultStyleResourceUri = new Uri(
            "ms-appx:///SimplePad.UWP.UI/Controls/AppTabViewItem.xaml"
        );
    }

    public bool IsModified
    {
        get => (bool)GetValue(IsModifiedProperty);
        set => SetValue(IsModifiedProperty, value);
    }
}
