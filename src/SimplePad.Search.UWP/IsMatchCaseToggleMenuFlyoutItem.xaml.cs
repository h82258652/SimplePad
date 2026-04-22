using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Search;

public sealed partial class IsMatchCaseToggleMenuFlyoutItem : ToggleMenuFlyoutItem
{
    private readonly CoreDispatcher _dispatcher;
    private readonly ISearchSettings _searchSettings;

    public IsMatchCaseToggleMenuFlyoutItem()
    {
        _dispatcher = Dispatcher;
        _searchSettings = ServiceLocator.Current.GetRequiredService<ISearchSettings>();

        InitializeComponent();

        UpdateIsChecked();

        _searchSettings.IsMatchCaseChanged += OnSearchSettingsIsMatchCaseChanged;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _searchSettings.IsMatchCase = IsChecked;
    }

    private async void OnSearchSettingsIsMatchCaseChanged(object? sender, bool e)
    {
        await _dispatcher.SafeRunAsync(UpdateIsChecked);
    }

    private void UpdateIsChecked()
    {
        IsChecked = _searchSettings.IsMatchCase;
    }
}