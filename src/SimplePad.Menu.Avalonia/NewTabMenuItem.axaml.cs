using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SimplePad.Tabs;

namespace SimplePad.Menu;

public sealed partial class NewTabMenuItem : MenuItem
{
    public static readonly StyledProperty<TabRoot?> TabRootProperty = AvaloniaProperty.Register<NewTabMenuItem, TabRoot?>(nameof(TabRoot));

    public NewTabMenuItem()
    {
        InitializeComponent();
    }

    public TabRoot? TabRoot
    {
        get => GetValue(TabRootProperty);
        set => SetValue(TabRootProperty, value);
    }

    private void OnClick(object? sender, RoutedEventArgs e)
    {
        TabRoot?.AddBlankTab();
    }
}