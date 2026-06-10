using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Tabs;

namespace SimplePad.Menu;

public sealed partial class SaveMenuItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TabProperty = DependencyProperty.Register(
        nameof(Tab),
        typeof(Tab),
        typeof(SaveMenuItem),
        null);

    private readonly TabManager _tabManager;

    public SaveMenuItem()
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
            _ = await _tabManager.SaveAsync(tab);
        }
    }
}