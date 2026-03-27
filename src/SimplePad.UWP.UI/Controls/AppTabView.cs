using System;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace SimplePad.UWP.UI.Controls;

public sealed partial class AppTabView : TabView
{
    public static readonly DependencyProperty TitleBarProperty = DependencyProperty.Register(
        nameof(TitleBar),
        typeof(UIElement),
        typeof(AppTabView),
        null
    );

    public AppTabView()
    {
        DefaultStyleKey = typeof(AppTabView);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.UWP.UI/Controls/AppTabView.xaml");
    }

    public UIElement? TitleBar
    {
        get => (UIElement?)GetValue(TitleBarProperty);
        set => SetValue(TitleBarProperty, value);
    }
}
