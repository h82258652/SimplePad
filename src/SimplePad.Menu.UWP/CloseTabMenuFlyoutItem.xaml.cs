using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Tabs;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class CloseTabMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TabProperty = DependencyProperty.Register(
        nameof(Tab),
        typeof(Tab),
        typeof(CloseTabMenuFlyoutItem),
        null);

    private readonly TabManager _tabManager;

    public CloseTabMenuFlyoutItem()
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