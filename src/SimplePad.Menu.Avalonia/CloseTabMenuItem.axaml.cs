using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Tabs;

namespace SimplePad.Menu;

public partial class CloseTabMenuItem : MenuItem
{
    public static readonly StyledProperty<Tab?> TabProperty =
        AvaloniaProperty.Register<CloseTabMenuItem, Tab?>(nameof(Tab));

    private readonly TabManager _tabManager;

    public CloseTabMenuItem()
    {
        _tabManager = ServiceLocator.Current.GetRequiredService<TabManager>();

        InitializeComponent();
    }

    public Tab? Tab
    {
        get => GetValue(TabProperty);
        set => SetValue(TabProperty, value);
    }

    private async void OnClick(object? sender, RoutedEventArgs e)
    {
        if (Tab is { } tab)
        {
            await _tabManager.CloseAsync(tab);
        }
    }
}