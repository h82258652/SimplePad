using System;
using System.Numerics;
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
        Visibility = Visibility.Visible;
        Visual visual = ElementCompositionPreview.GetElementVisual(this);
        Compositor compositor = visual.Compositor;

        Vector3KeyFrameAnimation scaleAnimation = compositor.CreateVector3KeyFrameAnimation();
        scaleAnimation.InsertKeyFrame(0f, Vector3.Zero);
        scaleAnimation.InsertKeyFrame(1f, Vector3.One);
        scaleAnimation.Duration = TimeSpan.FromSeconds(0.3);

        ScalarKeyFrameAnimation opacityAnimation = compositor.CreateScalarKeyFrameAnimation();
        opacityAnimation.InsertKeyFrame(0f, 0f);
        opacityAnimation.InsertKeyFrame(1f, 1f);
        opacityAnimation.Duration = TimeSpan.FromSeconds(0.3);

        visual.StartAnimation(nameof(visual.Scale), scaleAnimation);
        visual.StartAnimation(nameof(visual.Opacity), opacityAnimation);
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