using Avalonia;
using Avalonia.Controls;
using System;

namespace SimplePad.Tabs;

public sealed partial class AppTabView : TabControl
{
    public static readonly StyledProperty<TabRoot?> TabRootProperty = AvaloniaProperty.Register<AppTabView, TabRoot?>(nameof(TabRoot));

    static AppTabView()
    {
        TabRootProperty.Changed.AddClassHandler<AppTabView>(OnTabRootChanged);
    }

    public AppTabView()
    {
        InitializeComponent();
    }

    public TabRoot? TabRoot
    {
        get => GetValue(TabRootProperty);
        set => SetValue(TabRootProperty, value);
    }

    private static void OnTabRootChanged(AppTabView view, AvaloniaPropertyChangedEventArgs args)
    {
        throw new NotImplementedException();
    }
}