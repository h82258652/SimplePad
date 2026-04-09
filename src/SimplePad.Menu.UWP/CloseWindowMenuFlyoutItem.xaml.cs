using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class CloseWindowMenuFlyoutItem : MenuFlyoutItem
{
    public CloseWindowMenuFlyoutItem()
    {
        InitializeComponent();
    }

    private async void OnClick(object sender, RoutedEventArgs e)
    {
        _ = await ApplicationView.GetForCurrentView().TryConsolidateAsync();
    }
}
