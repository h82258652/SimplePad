using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Tabs;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class SaveAllMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TabRootProperty = DependencyProperty.Register(
        nameof(TabRoot),
        typeof(TabRoot),
        typeof(SaveAllMenuFlyoutItem),
        null);

    private readonly TabManager _tabManager;

    public SaveAllMenuFlyoutItem()
    {
        _tabManager = ServiceLocator.Current.GetRequiredService<TabManager>();

        InitializeComponent();
    }

    public TabRoot? TabRoot
    {
        get => (TabRoot?)GetValue(TabRootProperty);
        set => SetValue(TabRootProperty, value);
    }

    private async void OnClick(object sender, RoutedEventArgs e)
    {
        if (TabRoot is not { } tabRoot)
        {
            return;
        }

        foreach (Tab tab in tabRoot.Tabs)
        {
            if (!await _tabManager.SaveAsync(tab))
            {
                return;
            }
        }
    }
}