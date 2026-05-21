using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Tabs;

namespace SimplePad.Menu;

public partial class CloseTabMenuItem : MenuItem
{
    public static readonly DependencyProperty TabProperty = DependencyProperty.Register(
        nameof(Tab),
        typeof(Tab),
        typeof(CloseTabMenuItem),
        null);

    private readonly TabManager _tabManager;

    public CloseTabMenuItem()
    {
        _tabManager = ServiceLocator.Current.GetRequiredService<TabManager>();

        InitializeComponent();
    }

    public Tab? Tab
    {
        get => (Tab?)GetValue(TabProperty);
        set => SetValue(TabProperty, value);
    }

    private async void OnClick(object sender, RoutedEventArgs e)
    {
        if (Tab is { } tab)
        {
            await _tabManager.CloseAsync(tab);
        }
    }
}