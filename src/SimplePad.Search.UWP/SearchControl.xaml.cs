using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace SimplePad.Search;

public sealed partial class SearchControl : UserControl
{
    private readonly UWPSearchNotificationService _searchNotificationService;
    private readonly SearchViewState _searchViewState;

    public SearchControl()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();
        _searchNotificationService = ServiceLocator.Current.GetRequiredService<UWPSearchNotificationService>();

        InitializeComponent();
        _searchNotificationService.Configure(ShowNotificationFlyout, HideNotificationFlyout, SetNotificationText);

        UpdateVisibility();

        _searchViewState.IsVisibleChanged += OnSearchViewStateIsVisibleChanged;
    }

    private void HideNotificationFlyout()
    {
        NotificationFlyout.Hide();
    }

    private void OnReplaceAllButtonClick(object sender, RoutedEventArgs e)
    {
        ReplaceAllCommand replaceAllCommand = new();
        replaceAllCommand.Execute(null);
    }

    private void OnSearchViewStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateVisibility();
    }

    private void SetNotificationText(string notificationText)
    {
        NotificationText.Text = notificationText;
    }

    private void ShowNotificationFlyout()
    {
        FlyoutBase.ShowAttachedFlyout(RootContainer);
    }

    private void UpdateVisibility()
    {
        if (_searchViewState.IsVisible)
        {
            Visibility = Visibility.Visible;
        }
        else
        {
            Visibility = Visibility.Collapsed;
        }
    }
}