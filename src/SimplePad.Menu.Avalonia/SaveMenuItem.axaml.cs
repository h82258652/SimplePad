using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Tabs;

namespace SimplePad.Menu;

public partial class SaveMenuItem : MenuItem
{
    public static readonly StyledProperty<Tab?> TabProperty =
        AvaloniaProperty.Register<SaveMenuItem, Tab?>(nameof(Tab));

    private readonly TabManager _tabManager;

    public SaveMenuItem()
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
            _ = await _tabManager.SaveAsync(tab);
        }
    }
}