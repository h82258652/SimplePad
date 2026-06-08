using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using SimplePad.Core;
using System;

namespace SimplePad.Search;

public sealed partial class SearchControl : UserControl
{
    private readonly WinUISearchNotificationService _searchNotificationService;
    private readonly SearchViewState _searchViewState;

    public SearchControl()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();
        _searchNotificationService = ServiceLocator.Current.GetRequiredService<UWPSearchNotificationService>();

        InitializeComponent();
        _searchNotificationService.Configure(ShowNotificationFlyout, HideNotificationFlyout, SetNotificationText);

        Visibility = _searchViewState.IsVisible ? Visibility.Visible : Visibility.Collapsed;

        _searchViewState.IsVisibleChanged += OnSearchViewStateIsVisibleChanged;
    }

    private void HideNotificationFlyout()
    {
        NotificationFlyout.Hide();
    }

    private void OnSearchViewStateIsVisibleChanged(object? sender, bool e)
    {
        throw new NotImplementedException();
    }

    private void SetNotificationText(string notificationText)
    {
        NotificationText.Text = notificationText;
    }

    private void ShowNotificationFlyout()
    {
        FlyoutBase.ShowAttachedFlyout(RootContainer);
    }
}