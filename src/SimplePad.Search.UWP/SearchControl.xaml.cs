using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System.Threading.Tasks;
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
        UpdateGgggg();
        UpdateSearchTextBox();

        _searchViewState.IsVisibleChanged += OnSearchViewStateIsVisibleChanged;
        _searchViewState.IsReplaceModeChanged += OnSearchViewStateIsReplaceModeChanged;
        _searchViewState.SearchTextChanged += OnSearchViewStateSearchTextChanged;
    }

    private void HideNotificationFlyout()
    {
        NotificationFlyout.Hide();
    }

    private async void OnReplaceAllButtonClick(object sender, RoutedEventArgs e)
    {
        NotificationFlyout.Hide();

        await Task.Delay(3000);

        FlyoutBase.ShowAttachedFlyout(RootGrid);

        await Task.Delay(3000);

        NotificationFlyout.Hide();
        //new ReplaceAllCommand().Execute(null);
    }

    private void OnSearchTextBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        _searchViewState.SearchText = SearchTextBox.Text;
    }

    private void OnSearchViewStateIsReplaceModeChanged(object? sender, bool e)
    {
        UpdateGgggg();
    }

    private void OnSearchViewStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateVisibility();
    }

    private void OnSearchViewStateSearchTextChanged(object? sender, string e)
    {
        UpdateSearchTextBox();
    }

    private void SetNotificationText(string notificationText)
    {
        NotificationText.Text = notificationText;
    }

    private void ShowNotificationFlyout()
    {
        FlyoutBase.ShowAttachedFlyout(RootGrid);
    }

    private void UpdateGgggg()
    {
        if (_searchViewState.IsReplaceMode)
        {
            ReplaceModePanel.Visibility = Visibility.Visible;
        }
        else
        {
            ReplaceModePanel.Visibility = Visibility.Collapsed;
        }
    }

    private void UpdateSearchTextBox()
    {
        SearchTextBox.Text = _searchViewState.SearchText;
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