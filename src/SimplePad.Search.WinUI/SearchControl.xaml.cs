using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Hosting;
using SimplePad.Core;

namespace SimplePad.Search;

public sealed partial class SearchControl : UserControl
{
    private readonly WinUISearchNotificationService _searchNotificationService;
    private readonly SearchViewState _searchViewState;

    public SearchControl()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();
        _searchNotificationService = ServiceLocator.Current.GetRequiredService<WinUISearchNotificationService>();

        InitializeComponent();
        _searchNotificationService.Configure(ShowNotificationFlyout, HideNotificationFlyout, SetNotificationText);

        Visibility = _searchViewState.IsVisible ? Visibility.Visible : Visibility.Collapsed;

        _searchViewState.IsVisibleChanged += OnSearchViewStateIsVisibleChanged;
    }

    private void Hide()
    {
        Visual visual = ElementCompositionPreview.GetElementVisual(this);
        Compositor compositor = visual.Compositor;

        throw new NotImplementedException();
    }

    private void HideNotificationFlyout()
    {
        NotificationFlyout.Hide();
    }

    private void OnSearchViewStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateVisibility();
    }

    private void SetNotificationText(string notificationText)
    {
        NotificationText.Text = notificationText;
    }

    private void Show()
    {
        throw new NotImplementedException();
    }

    private void ShowNotificationFlyout()
    {
        FlyoutBase.ShowAttachedFlyout(RootContainer);
    }

    private void UpdateVisibility()
    {
        if (_searchViewState.IsVisible)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}