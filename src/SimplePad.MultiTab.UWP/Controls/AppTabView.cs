using System;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace SimplePad.MultiTab.UWP.Controls;

public sealed partial class AppTabView : TabView
{
    public AppTabView()
    {
        DefaultStyleKey = typeof(AppTabView);
        DefaultStyleResourceUri = new Uri(
            "ms-appx:///SimplePad.MultiTab.UWP/Controls/AppTabView.xaml"
        );
    }

    public UIElement? TitleBar
    {
        get => (UIElement?)GetValue(TitleBarProperty);
        set => SetValue(TitleBarProperty, value);
    }

    public static readonly DependencyProperty TitleBarProperty = DependencyProperty.Register(
        nameof(TitleBar),
        typeof(UIElement),
        typeof(AppTabView),
        null);
}
