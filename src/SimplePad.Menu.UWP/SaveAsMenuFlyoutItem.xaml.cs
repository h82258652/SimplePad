using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Tabs;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class SaveAsMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TabProperty = DependencyProperty.Register(
        nameof(Tab),
        typeof(Tab),
        typeof(SaveAsMenuFlyoutItem),
        null);

    private readonly TabManager _tabManager;

    public SaveAsMenuFlyoutItem()
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
            await _tabManager.SaveAsync(tab);
        }
    }
}