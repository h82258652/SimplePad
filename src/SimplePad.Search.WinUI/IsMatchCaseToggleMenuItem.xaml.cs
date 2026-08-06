using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;

namespace SimplePad.Search;

public sealed partial class IsMatchCaseToggleMenuItem : ToggleMenuFlyoutItem
{
    private readonly ISearchSettings _searchSettings;

    public IsMatchCaseToggleMenuItem()
    {
        _searchSettings = ServiceLocator.Current.GetRequiredService<ISearchSettings>();

        InitializeComponent();

        UpdateIsChecked();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _searchSettings.IsMatchCase = IsChecked;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _searchSettings.IsMatchCaseChanged += OnSearchSettingsIsMatchCaseChanged;

        UpdateIsChecked();
    }

    private void OnSearchSettingsIsMatchCaseChanged(object? sender, bool e)
    {
        UpdateIsChecked();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _searchSettings.IsMatchCaseChanged -= OnSearchSettingsIsMatchCaseChanged;
    }

    private void UpdateIsChecked()
    {
        IsChecked = _searchSettings.IsMatchCase;
    }
}