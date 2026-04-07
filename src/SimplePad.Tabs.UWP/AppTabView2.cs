using System;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace SimplePad.Tabs;

public sealed partial class AppTabView2 : TabView
{
    public static readonly DependencyProperty TitleBarProperty = DependencyProperty.Register(
        nameof(TitleBar),
        typeof(UIElement),
        typeof(AppTabView2),
        null);

    public AppTabView2()
    {
        DefaultStyleKey = typeof(AppTabView2);
        DefaultStyleResourceUri = new Uri(
            "ms-appx:///SimplePad.Tabs.UWP/AppTabView.xaml"
        );

        AddTabButtonClick += OnAddTabButtonClick;
        TabCloseRequested += OnTabCloseRequested;
    }

    public static readonly DependencyProperty TabRootProperty = DependencyProperty.Register(
        nameof(TabRoot),
        typeof(TabRoot),
        typeof(AppTabView2),
        null);

    public TabRoot? TabRoot
    {
        get => (TabRoot?)GetValue(TabRootProperty);
        set => SetValue(TabRootProperty, value);
    }

    private void OnTabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
    }

    private void OnAddTabButtonClick(TabView sender, object args)
    {
        TabRoot?.Add();
    }

    public UIElement? TitleBar
    {
        get => (UIElement?)GetValue(TitleBarProperty);
        set => SetValue(TitleBarProperty, value);
    }
}
